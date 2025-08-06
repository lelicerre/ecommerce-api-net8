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

    public async Task<IEnumerable<Produto>> ListarAsync()
    {
        var sql = "SELECT * FROM Produtos WHERE Deletado = 0";
        using var conn = _provider.GetConnection();
        return await conn.QueryAsync<Produto>(sql);
    }

    public async Task InserirAsync(Produto produto)
    {
        var sql = @"INSERT INTO Produtos (Id, Codigo, Descricao, CodigoDepartamento, Preco, Status, Deletado)
                    VALUES (@Id, @Codigo, @Descricao, @CodigoDepartamento, @Preco, @Status, 0)";
        using var conn = _provider.GetConnection();
        await conn.ExecuteAsync(sql, produto);
    }

    public async Task AtualizarAsync(Produto produto)
    {
        var sql = @"UPDATE Produtos SET Codigo = @Codigo, Descricao = @Descricao,
                    CodigoDepartamento = @CodigoDepartamento, Preco = @Preco, Status = @Status
                    WHERE Id = @Id";
        using var conn = _provider.GetConnection();
        await conn.ExecuteAsync(sql, produto);
    }

    public async Task DeletarAsync(Guid id)
    {
        var sql = "UPDATE Produtos SET Deletado = 1 WHERE Id = @Id";
        using var conn = _provider.GetConnection();
        await conn.ExecuteAsync(sql, new { Id = id });
    }
}