using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Models;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("departamentos")]
public class DepartamentosController : ControllerBase
{
    [HttpGet]
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