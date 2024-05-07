using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public class DatabaseConnection
{
    private SqlConnectionOptions _options;

    public DatabaseConnection(SqlConnectionOptions options)
    {
        _options = options;
    }

    private async Task Connect()
    {
        if (Connection is null)
        {
            Connection = new SqlConnection(_options.ConnectionString);
            await Connection.OpenAsync();
            return;
        }
        if (Connection.State == System.Data.ConnectionState.Closed
            || Connection.State == System.Data.ConnectionState.Broken)
        {
            Connection = new SqlConnection(_options.ConnectionString);
            await Connection.OpenAsync();
        }
    }

    private SqlConnection Connection { get; set; }

    public async Task<SqlConnection> GetConnection()
    {
        await Connect();

        return Connection;
    }
}
