using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Data.SqlClient;
using Humanizer;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Mediator;

namespace QuickCode.Demo.Common.Extensions;

public static class Extensions
{ 
    public static bool IsRouteMatch(this string routeTemplate, string actualPath)
    {
        var template = TemplateParser.Parse(routeTemplate);
        var matcher = new TemplateMatcher(template, new RouteValueDictionary());
        return matcher.TryMatch(actualPath, new RouteValueDictionary());
    }
    
    public static bool IsRouteMatch(this string path, List<string> paths)
    {
        return paths.Exists(item => item.IsRouteMatch(path));
    }

    public static List<Type> GetAssemblyTypes(this AppDomain appDomain, string suffix, string namespaceSuffix)
    {
        return appDomain.GetAssembliesWithSuffix(suffix)
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.Namespace?.EndsWith($"{suffix}.{namespaceSuffix}") == true)
            .ToList();
    }

    public static RepoResponse<T> LogExceptionAndCreateResponse<T>(this ILogger logger, Exception ex, string repoName, string operation, int defaultErrorCode = 404)
    {
        logger.LogError("{RepoName} Exception {Error}", $"{repoName} {operation}", ex.Message);
        var code = ex switch
        {
            SqlException { Number: 2627 } => 999,
            SqlException => 998,
            _ => defaultErrorCode
        };
        return new RepoResponse<T> { Code = code, Message = ex.ToString() };
    }

    public static IEnumerable<Assembly> GetAssembliesWithSuffix(this AppDomain appDomain, string suffix)
    {
        return appDomain.GetAssemblies()
            .Where(assembly => assembly.GetName().Name?.EndsWith(suffix) == true);
    }
    
    public static IServiceCollection AddQuickCodeRepositories(this IServiceCollection services, Type writeDbContextType)
    {
        var namespacePrefix = writeDbContextType.GetNamespacePrefix();
        var repositoryAssemblyName = writeDbContextType.Assembly
            .GetReferencedAssemblies().First(i => i.Name!.StartsWith(namespacePrefix) && i.Name!.EndsWith(".Application"));
        var repositoryAssembly = Assembly.Load(repositoryAssemblyName);
        
        var repoInterfaces = repositoryAssembly.GetTypes()
            .Where(i => i.Name.EndsWith("Repository") && i.IsInterface);
                
        foreach (var interfaceType in repoInterfaces)
        {
            var implementationType = writeDbContextType.Assembly.GetTypes()
                                         .FirstOrDefault(i => i.Name == interfaceType.Name[1..])
                                     ?? throw new InvalidOperationException($"Implementation not found for {interfaceType.Name}");
                    
            services.AddScoped(interfaceType, implementationType);
        }
            
        return services;
    }
    
    public static string GetNamespacePrefix(this Type obj)
    {
        var namespaceParts = obj.Assembly.FullName!.Split('.');
        return string.Join('.', namespaceParts.Take(3));
    }
    
    public static void AddQuickCodeMediator<T>(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.AddScoped<IMediator, QuickCodeMediator>();
        var namespacePrefix = typeof(T).GetNamespacePrefix();
        
        var repositoryAssemblyName = assemblies.First(assembly =>
                assembly.GetName().Name!.StartsWith(namespacePrefix) &&
                assembly.GetName().Name!.EndsWith(".Persistence"))
            .GetReferencedAssemblies()
            .First(i => i.Name!.StartsWith(namespacePrefix) && i.Name!.EndsWith(".Application"));
        var assembly = Assembly.Load(repositoryAssemblyName);
        
        var handlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => new { HandlerType = t, InterfaceType = i }));

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.InterfaceType, handler.HandlerType);
        }

        var notificationHandlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
                .Select(i => new { HandlerType = t, InterfaceType = i }));

        foreach (var handler in notificationHandlerTypes)
        {
            services.AddScoped(handler.InterfaceType, handler.HandlerType);
        }
    }
    
    public static string ToJson<T>(this T obj)
    {
        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true, 
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };
        return System.Text.Json.JsonSerializer.Serialize(obj, options);
    }

    public static bool IsInList(this string obj, params string[] list)
    {
        return list.Contains(obj);
    }
    
    public static IList DeserializeToList(this List<Dictionary<string, object>> rawList, Type entityType)
    {
        var resultList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(entityType))!;
    
        foreach (var dict in rawList)
        {
            var jObject = JObject.FromObject(dict);
            var obj = jObject.ToObject(entityType);
        
            if (obj != null)
                resultList.Add(obj);
        }

        return resultList;
    }
      
    public static IList DeserializeSectionToList(this JObject jObject, string sectionName, Type entityType)
    {
        var section = jObject[sectionName];

        var resultList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(entityType))!;
        if (section is not JArray array)
            return resultList;

        foreach (var item in array)
        {
            var obj = item.ToObject(entityType);
            
            if (obj != null)
                resultList.Add(obj);
        }

        return resultList;
    }

    public static string GetPascalCase(this string name)
    {
        if (name.IsPascalCase()) return name;

        name = name.ToLower(CultureInfo.CreateSpecificCulture("en-US"));
        return Regex.Replace(name, @"^\w|_\w", match => match.Value.Replace("_", "").ToUpper(CultureInfo.CreateSpecificCulture("en-US")));
    }
    
    public static bool IsPascalCase(this string text)
    {
        if (text.ToUpper().Equals(text)) return false;
            
        var words = text.Split([' ', '_'], StringSplitOptions.RemoveEmptyEntries);
        return words.All(word => char.IsUpper(word[0]));
    }
    
    public static string GetPropertyTypeName(this Type type, string activeProvider)
    {
        var typeName = type.Name;
        if (type.CustomAttributes.Any(i => i.AttributeType.Name.Equals("NullableAttribute")))
        {
            typeName = $"{typeName}?";
        }
        
        if (type.FullName!.StartsWith("System.Nullable"))
        {
            typeName = $"{type.GenericTypeArguments[0].Name}?";
        }

        typeName = typeName.Replace("String", "string")
            .Replace("Int32", "int")
            .Replace("Int64", "int")
            .Replace("DateTime", "datetime")
            .Replace("Int16", "int");
        
        if (activeProvider.Equals("Microsoft.EntityFrameworkCore.SqlServer"))
        {
            typeName = typeName.Replace("Boolean", "bit");
        }

        return typeName;
    }
    
    public static void ParseJsonAsInitialData<T>(this MigrationBuilder migrationBuilder, List<string> fileList)
    {
        var namespacePrefix = typeof(T).GetNamespacePrefix();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var domainEntityTypeAssembly = assemblies.First(assembly =>
                assembly.GetName().Name!.StartsWith(namespacePrefix) && assembly.GetName().Name!.EndsWith(".Domain"));
        
        foreach (var file in fileList)
        {
            var fileContent = File.ReadAllText(file);
            try
            {
                var models = JObject.Parse(fileContent).ToObject<Dictionary<string, List<Dictionary<string, object>>>>();
                foreach (var tableName in models!.Keys)
                {
                    var entityType = domainEntityTypeAssembly.GetType($"{namespacePrefix}.Domain.Entities.{tableName.Singularize().GetPascalCase()}");
                    if (entityType is null) continue;
                    
                    var columnNames = models[tableName][0].Keys.ToArray();
                    var objects = new object[models[tableName].Count, columnNames.Length];
                    var columnTypes = columnNames.Select(i =>
                        entityType.GetProperty(i.GetPascalCase())!.PropertyType.GetPropertyTypeName(migrationBuilder
                            .ActiveProvider!).TrimEnd('?'));
                    
                    for (var rowCounter = 0; rowCounter < models[tableName].Count; rowCounter++)
                    {
                        var rows = models[tableName][rowCounter];
                        for (var index = 0; index < columnNames.Length; index++)
                        {
                            var column = columnNames[index];
                            var colType = entityType.GetProperty(column.GetPascalCase())!;
                            if (colType.PropertyType == typeof(bool))
                            {
                                rows[column] = Convert.ToBoolean(rows[column]);
                            }
                            objects.SetValue(rows[column], rowCounter, index);
                        }
                    }

                    migrationBuilder.InsertData(tableName.PluralizeForce(), columns: columnNames, columnTypes: columnTypes.ToArray(),
                        values: objects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
    }
    
    public static List<string> GetMigrationDataFiles(this Type type)
    {
        var splitValues = type.Namespace!.Split(".");
        var moduleName = splitValues[2];
        var projectName = splitValues[1];
        var currentDir = Directory.GetCurrentDirectory();
        currentDir = currentDir.Split(Path.DirectorySeparatorChar).Any(i => i.Equals(moduleName))
            ? currentDir[..currentDir.IndexOf(moduleName, StringComparison.Ordinal)]
            : $@"/src/Modules/";
        

        var path = $@"{currentDir}{moduleName}/Infrastructure/QuickCode.{projectName}.{moduleName}.Persistence/Migrations/InitialData";
        if (!Directory.Exists(path))
        {
            path = $"/app/Migrations/InitialData";
        }

        return Directory.Exists(path) ? Directory.GetFiles(path, "*.json").OrderBy(i => i).ToList() : new List<string>();
    }
    
    public static string GetMigrationDataPath(this Type type)
    {
        var splitValues = type.Namespace!.Split(".");
        var moduleName = splitValues[2];
        var projectName = splitValues[1];
        var currentDir = Directory.GetCurrentDirectory();
        currentDir = currentDir.Split(Path.DirectorySeparatorChar).Any(i => i.Equals(moduleName))
            ? currentDir[..currentDir.IndexOf(moduleName, StringComparison.Ordinal)]
            : $@"/src/Modules/";

        var path = $@"{currentDir}{moduleName}/Infrastructure/QuickCode.{projectName}.{moduleName}.Persistence/Migrations/InitialData/{moduleName}Data.json";
        if (!File.Exists(path))
        {
            path = $"/app/Migrations/InitialData/{moduleName}Data.json";
        }

        return path;
    }
}