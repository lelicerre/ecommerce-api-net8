using System.Data;
using Dapper;

namespace EcommerceApi.Data;

public class DbInitializer
{
    private readonly IDbConnection _connection;

    public DbInitializer(DbConnectionProvider provider)
    {
        _connection = provider.GetConnection();
    }

    public void Initialize()
    {
        var sql = @"
            CREATE TABLE IF NOT EXISTS Produtos (
                Id TEXT PRIMARY KEY,
                Code TEXT NOT NULL,
                Description TEXT NOT NULL,
                Department TEXT NOT NULL,
                Price REAL NOT NULL,
                Status INTEGER NOT NULL,
                Deletado INTEGER NOT NULL
            );";

        _connection.Execute(sql);
    }
}