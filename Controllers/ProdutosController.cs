using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoRepository _repo;

    public ProdutosController(ProdutoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var (produtos, total) = await _repo.ListarAsync(page, size);
        return Ok(new { data = produtos, total, page, size });
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(Produto produto)
    {
        await _repo.InserirAsync(produto);
        return CreatedAtAction(nameof(Listar), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, Produto produto)
    {
        produto.Id = id;
        await _repo.AtualizarAsync(produto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(string id)
    {
        await _repo.DeletarAsync(id);
        return NoContent();
    }
}