using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoRepository _repo;

    public ProdutosController(ProdutoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        var (produtos, total) = await _repo.ListarAsync(page + 1, size);
        var totalPages = (int)Math.Ceiling((double)total / size);

        return Ok(new
        {
            content = produtos,
            pageable = new
            {
                pageNumber = page,
                pageSize = size,
                sort = new { empty = true, sorted = false, unsorted = true },
                offset = page * size,
                paged = true,
                unpaged = false
            },
            last = page + 1 >= totalPages,
            totalElements = total,
            totalPages = totalPages,
            size,
            number = page,
            first = page == 0,
            numberOfElements = produtos.Count(),
            empty = !produtos.Any()
        });
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