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
}