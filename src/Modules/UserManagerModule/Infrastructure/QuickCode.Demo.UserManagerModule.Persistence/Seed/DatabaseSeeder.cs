using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.Common.Mediator;
using QuickCode.Demo.UserManagerModule.Persistence.Contexts;
using QuickCode.Demo.UserManagerModule.Persistence.Migrations;
using QuickCode.Demo.UserManagerModule.Application.Features;

namespace QuickCode.Demo.UserManagerModule.Persistence.Seed;

public static class DatabaseSeeder
{
    private static Dictionary<string, List<string>> UniqueColumnMap => new()
    {
        { "PortalPermissions", ["Name"] },
        { "PortalPermissionGroups", ["PortalPermissionName", "PermissionGroupName","PortalPermissionTypeId"] },
        { "PortalMenus", ["Key"] },
        { "TableComboboxSettings", ["TableName", "IdColumn"] },
        { "ApiMethodDefinitions", ["Key"] },
        { "ApiPermissionGroups", ["ApiMethodDefinitionKey", "PermissionGroupName"] }
    };
    
    private static readonly Assembly DomainAssembly =
        AppDomain.CurrentDomain.GetAssemblies()
            .First(a => a.GetName().Name!.EndsWith(".Domain"));
    
    private static async Task<Dictionary<string, List<Dictionary<string, object>>>> FetchCurrentDataFromDatabaseAsync(IServiceProvider serviceProvider, ILogger<WriteDbContext> logger)
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var dbModels = new Dictionary<string, List<Dictionary<string, object>>>();
        try
        {
            var responsePortalPermissionGroups = await mediator.Send(new PortalPermissionGroupsListQuery(null, null));
            var responsePortalPermissions = await mediator.Send(new PortalPermissionsListQuery(null, null));
            var responsePortalMenus = await mediator.Send(new PortalMenusListQuery(null, null));
            var responseApiMethodDefinitions = await mediator.Send(new ApiMethodDefinitionsListQuery(null, null));
            var responseApiPermissionGroups = await mediator.Send(new ApiPermissionGroupsListQuery(null, null));
            var responseTableComboboxSettings = await mediator.Send(new TableComboboxSettingsListQuery(null, null));
            
            dbModels.Add("ApiPermissionGroups", ConvertJsonToDictionary(responseApiPermissionGroups.Value.ToJson()));
            dbModels.Add("ApiMethodDefinitions", ConvertJsonToDictionary(responseApiMethodDefinitions.Value.ToJson()));
            dbModels.Add("PortalPermissionGroups", ConvertJsonToDictionary(responsePortalPermissionGroups.Value.ToJson()));
            dbModels.Add("PortalPermissions", ConvertJsonToDictionary(responsePortalPermissions.Value.ToJson()));
            dbModels.Add("PortalMenus", ConvertJsonToDictionary(responsePortalMenus.Value.ToJson()));
            dbModels.Add("TableComboboxSettings", ConvertJsonToDictionary(responseTableComboboxSettings.Value.ToJson()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration data fetch failed.");
            return dbModels;
        }

        return dbModels;
    }

    private static async Task<Dictionary<string, List<Dictionary<string, object>>>> ReadDataFromMigrationFilesAsync(ILogger<WriteDbContext> logger)
    {
        var fileList = typeof(BaseData).GetMigrationDataFiles();
        var models = new Dictionary<string, List<Dictionary<string, object>>>();

        foreach (var filePath in fileList)
        {
            if (!File.Exists(filePath))
            {
                logger.LogWarning("Migration file not found: {FilePath}", filePath);
                continue;
            }

            var json = await File.ReadAllTextAsync(filePath);
            var jsonObject = JObject.Parse(json);
            var fileModels = jsonObject.ToObject<Dictionary<string, List<Dictionary<string, object>>>>();
            if (fileModels is null) continue;

            foreach (var kvp in fileModels)
            {
                if (!models.ContainsKey(kvp.Key))
                {
                    models[kvp.Key] = [];
                }

                models[kvp.Key].AddRange(kvp.Value);
            }
        }

        return models;
    }

    private static async Task<Dictionary<string, List<Dictionary<string, object>>>> CompareAndPrepareInsertDataAsync(IServiceProvider serviceProvider, ILogger<WriteDbContext> logger)
    {
        var result = new Dictionary<string, List<Dictionary<string, object>>>();

        var currentDbData = await FetchCurrentDataFromDatabaseAsync(serviceProvider, logger);
        var migrationFilesData = await ReadDataFromMigrationFilesAsync(logger);
        
        foreach (var (tableName, uniqueColumns) in UniqueColumnMap)
        {
            var rowsToInsert = FindMissingRowsByUniqueKeys(currentDbData, migrationFilesData, tableName, uniqueColumns);

            if (rowsToInsert.Count == 0)
            {
                logger.LogInformation("No Data for table: {Table}", tableName);
                continue;
            }
            
            result[tableName] = rowsToInsert;
        }
        
        return result;
    }

    public static async Task SeedAsync(WriteDbContext dbContext, IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<WriteDbContext>>();
        var insertData = await CompareAndPrepareInsertDataAsync(serviceProvider, logger);

        foreach (var tableName in insertData.Keys)
        {
            var entityType = DomainAssembly.GetType($"{DomainAssembly.GetName().Name}.Entities.{tableName.GetPascalCase()}");
            if (entityType is null) continue;

            var insertRows = insertData[tableName].DeserializeToList(entityType);
            if (insertRows.Count == 0) continue;

            var dbSet = dbContext.GetType()
                .GetMethod("Set", Type.EmptyTypes)!
                .MakeGenericMethod(entityType)
                .Invoke(dbContext, null);

            var addRangeMethod = dbSet!.GetType()
                .GetMethod("AddRange", [typeof(IEnumerable<>).MakeGenericType(entityType)]);
            if (addRangeMethod == null) continue;

            try
            {
                addRangeMethod.Invoke(dbSet, [insertRows]);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Seeded table: {Table} -> Inserted Row(s): {InsertedRow}", tableName, insertRows.Count);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "DbUpdateException on table {Table}", tableName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception on table {Table}", tableName);
            }
        }

        logger.LogInformation("✅ Migration data imported successfully.");
    }

    private static List<Dictionary<string, object>> ConvertJsonToDictionary(string json)
    {
        var parsedJson = JArray.Parse(json);
        return parsedJson.ToObject<List<Dictionary<string, object>>>()!;
    }

    private static List<Dictionary<string, object>> FindMissingRowsByUniqueKeys(
        Dictionary<string, List<Dictionary<string, object>>> existingModel,
        Dictionary<string, List<Dictionary<string, object>>> newModel,
        string tableName,
        List<string>? uniqueColumns = null)
    {
        if (!existingModel.ContainsKey(tableName))
            return [];

        var existingRows = existingModel.GetValueOrDefault(tableName, []);
        var newRows = newModel.GetValueOrDefault(tableName, []);
        var insertCandidates = new List<Dictionary<string, object>>();

        var existingJObjects = existingRows.Select(JObject.FromObject).ToList();

        foreach (var newRow in newRows)
        {
            var newJObject = JObject.FromObject(newRow);
            bool exists = uniqueColumns is null || uniqueColumns.Count == 0
                ? existingJObjects.Any(existing => JToken.DeepEquals(existing, newJObject))
                : existingJObjects.Any(existing => uniqueColumns.All(column =>
                    JToken.DeepEquals(existing[column], newJObject[column])));

            if (!exists)
                insertCandidates.Add(newRow);
        }

        return insertCandidates;
    }
}