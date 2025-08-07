using Dapper;
using EcommerceApi.Models;
using EcommerceApi.Data;

namespace EcommerceApi.Repositories;

public class DepartamentoRepository
{
    private readonly DbConnectionProvider _provider;

    public DepartamentoRepository(DbConnectionProvider provider)
    {
        _provider = provider;
    }

    public IEnumerable<Departamento> Listar()
    {
        using var conn = _provider.GetConnection();
        return conn.Query<Departamento>("SELECT Codigo, Descricao FROM Departamentos").ToList();
    }
}