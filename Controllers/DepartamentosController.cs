using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("departamentos")]
public class DepartamentosController : ControllerBase
{
    private readonly DepartamentoRepository _repo;

    public DepartamentosController(DepartamentoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Departamento>), StatusCodes.Status200OK)]
    public IActionResult Listar()
    {
        var departamentos = _repo.Listar();
        return Ok(departamentos);
    }
}