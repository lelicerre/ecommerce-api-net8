using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Models;

namespace EcommerceApi.Controllers;

/// <summary>
/// Controller responsável por retornar a lista de departamentos disponíveis.
/// </summary>
[ApiController]
[Route("departamentos")]
public class DepartamentosController : ControllerBase
{
    /// <summary>
    /// Lista todos os departamentos disponíveis para o cadastro de produtos.
    /// </summary>
    /// <returns>Lista de objetos contendo código e descrição de cada departamento.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Departamento>), StatusCodes.Status200OK)]
    public IActionResult Listar()
    {
        var departamentos = new List<Departamento>
        {
            new() { Codigo = "010", Descricao = "BEBIDAS" },
            new() { Codigo = "020", Descricao = "CONGELADOS" },
            new() { Codigo = "030", Descricao = "LATICINIOS" },
            new() { Codigo = "040", Descricao = "VEGETAIS" }
        };

        return Ok(departamentos);
    }
}