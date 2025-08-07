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
        SeedData();
    }

    private void InitializeSchema()
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Produtos (
                Id TEXT PRIMARY KEY,
                Code TEXT NOT NULL,
                Description TEXT NOT NULL,
                Department TEXT NOT NULL,
                Price REAL NOT NULL,
                Status INTEGER NOT NULL,
                Deletado INTEGER NOT NULL DEFAULT 0
            );

            CREATE TABLE IF NOT EXISTS Departamentos (
                Codigo TEXT PRIMARY KEY,
                Descricao TEXT NOT NULL
            );";
        cmd.ExecuteNonQuery();
    }

    private void SeedData()
    {
        using var checkCmd = _connection.CreateCommand();
        checkCmd.CommandText = "SELECT COUNT(*) FROM Departamentos;";
        var count = Convert.ToInt32(checkCmd.ExecuteScalar());

        if (count == 0)
        {
            using var insertCmd = _connection.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO Departamentos (Codigo, Descricao) VALUES 
                    ('010', 'BEBIDAS'),
                    ('020', 'CONGELADOS'),
                    ('030', 'LATICINIOS'),
                    ('040', 'VEGETAIS');";
            insertCmd.ExecuteNonQuery();
        }
    }

    public IDbConnection GetConnection() => _connection;

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}