using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace QuickCode.Demo.Common.Data;

public sealed class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private const string WriteConnectionStringName = "WriteConnectionString";
    private const string ReadConnectionStringName = "ReadConnectionString";

    public Task<DbConnection> CreateReadConnectionAsync(CancellationToken cancellationToken = default) =>
        CreateOpenConnectionAsync(ReadConnectionStringName, cancellationToken);

    public Task<DbConnection> CreateWriteConnectionAsync(CancellationToken cancellationToken = default) =>
        CreateOpenConnectionAsync(WriteConnectionStringName, cancellationToken);

    private async Task<DbConnection> CreateOpenConnectionAsync(string connectionStringName, CancellationToken cancellationToken)
    {
        var connectionString = configuration.GetConnectionString(connectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"Connection string '{connectionStringName}' is not configured.");

        var connection = CreateConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }

    private DbConnection CreateConnection(string connectionString)
    {
        var databaseType = configuration.GetSection("AppSettings:DatabaseType").Get<string>();
        return databaseType switch
        {
            "mssql" => new SqlConnection(connectionString),
            "postgresql" => new Npgsql.NpgsqlConnection(connectionString),
            "mysql" => new MySqlConnector.MySqlConnection(connectionString),
            "inMemory" => throw new NotSupportedException("Dapper repositories require a relational database connection."),
            _ => throw new NotSupportedException($"Database type '{databaseType}' is not supported.")
        };
    }
}
