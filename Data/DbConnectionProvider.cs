using Microsoft.Data.Sqlite;
using System.Data;

namespace EcommerceApi.Data;

public class DbConnectionProvider : IDisposable
{
    private readonly SqliteConnection _connection;

    public DbConnectionProvider()
    {
        _connection = new SqliteConnection("Data Source=file:memdb1?mode=memory&cache=shared");
        _connection.Open();
        InitializeSchema();
    }

    private void InitializeSchema()
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Produtos (
                Id TEXT PRIMARY KEY,
                Codigo TEXT,
                Descricao TEXT,
                CodigoDepartamento TEXT,
                Preco REAL,
                Status INTEGER,
                Deletado INTEGER DEFAULT 0
            );";
        cmd.ExecuteNonQuery();
    }

    public IDbConnection GetConnection() => _connection;

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}