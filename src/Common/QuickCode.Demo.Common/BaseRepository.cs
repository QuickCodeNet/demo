using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.Common;

public abstract class BaseRepository(ILogger logger, string repoName)
{
    protected async Task<RepoResponse<T>> ExecuteWithExceptionHandling<T>(string operation, Func<Task<RepoResponse<T>>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            return logger.LogExceptionAndCreateResponse<T>(ex, repoName, operation);
        }
    }

    protected IQueryable<T> ApplyPagination<T>(IQueryable<T> query, int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        return query.Skip(skip).Take(pageSize);
    }
    
    protected RepoResponse<T> CreateNotFoundResponse<T>(string message)
    {
        return new RepoResponse<T>
        {
            Code = 404,
            Message = message
        };
    }
    
    protected RepoResponse<List<T>> BuildListResponse<T>(IEnumerable<T> values, string notFoundMessage = "Not found")
    {
        return values?.Any() == true ? new RepoResponse<List<T>> { Value = values.ToList() } : CreateNotFoundResponse<List<T>>(notFoundMessage);
    }
    
    protected RepoResponse<T> BuildResponse<T>(T value, string notFoundMessage = "Not found")
    {
        return value is not null ? new RepoResponse<T> { Value = value } : CreateNotFoundResponse<T>(notFoundMessage);
    }

    protected RepoResponse<bool> BuildBoolResponse(bool exists, string notFoundMessage = "Not found")
    {
        return exists ? new RepoResponse<bool> { Value = exists } : CreateNotFoundResponse<bool>(notFoundMessage);
    }
    
    protected async Task<DbConnection> GetOpenConnectionAsync(DbContext dbContext)
    {
        var connection = dbContext.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        return connection;
    }
}