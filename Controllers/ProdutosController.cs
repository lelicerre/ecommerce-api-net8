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
    public async Task<IActionResult> Listar() => Ok(await _repo.ListarAsync());

    [HttpPost]
    public async Task<IActionResult> Inserir(Produto produto)
    {
        produto.Id = Guid.NewGuid();
        await _repo.InserirAsync(produto);
        return CreatedAtAction(nameof(Listar), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, Produto produto)
    {
        produto.Id = id;
        await _repo.AtualizarAsync(produto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _repo.DeletarAsync(id);
        return NoContent();
    }
}