using Dapper;
using EcommerceApi.Models;
using EcommerceApi.Data;

namespace EcommerceApi.Repositories;

public class ProdutoRepository
{
    private readonly DbConnectionProvider _provider;

    public ProdutoRepository(DbConnectionProvider provider)
    {
        _provider = provider;
    }

    public async Task<(IEnumerable<Produto> produtos, int total)> ListarAsync(int page, int size)
    {
        var offset = (page - 1) * size;
        var sql = "SELECT * FROM Produtos WHERE Deletado = 0 LIMIT @Size OFFSET @Offset";
        var countSql = "SELECT COUNT(*) FROM Produtos WHERE Deletado = 0";

        var conn = _provider.GetConnection();
        var produtos = await conn.QueryAsync<Produto>(sql, new { Size = size, Offset = offset });
        var total = await conn.ExecuteScalarAsync<int>(countSql);
        return (produtos, total);
    }

    public async Task InserirAsync(Produto produto)
    {
        produto.Id = Guid.NewGuid().ToString();

        var sql = @"INSERT INTO Produtos (Id, Code, Description, Department, Status, Price, Deletado)
                    VALUES (@Id, @Code, @Description, @Department, @Status, @Price, 0)";

        var conn = _provider.GetConnection();
        await conn.ExecuteAsync(sql, produto);
    }

    public async Task AtualizarAsync(Produto produto)
    {
        var sql = @"UPDATE Produtos 
                    SET Code = @Code, Description = @Description, Department = @Department, 
                        Status = @Status, Price = @Price 
                    WHERE Id = @Id";

        var conn = _provider.GetConnection();
        await conn.ExecuteAsync(sql, produto);
    }

    public async Task DeletarAsync(string id)
    {
        var sql = "UPDATE Produtos SET Deletado = 1 WHERE Id = @Id";
        var conn = _provider.GetConnection();
        await conn.ExecuteAsync(sql, new { Id = id });
    }
}