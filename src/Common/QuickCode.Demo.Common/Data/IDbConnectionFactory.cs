using System.Data.Common;

namespace QuickCode.Demo.Common.Data;

public interface IDbConnectionFactory
{
    Task<DbConnection> CreateReadConnectionAsync(CancellationToken cancellationToken = default);

    Task<DbConnection> CreateWriteConnectionAsync(CancellationToken cancellationToken = default);
}
