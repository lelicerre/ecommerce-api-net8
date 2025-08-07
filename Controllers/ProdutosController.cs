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
        var first = page == 0;
        var last = page + 1 >= totalPages;
        var numberOfElements = produtos.Count();
        var empty = numberOfElements == 0;

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
            last,
            totalElements = total,
            totalPages,
            size,
            number = page,
            sort = new { empty = true, sorted = false, unsorted = true },
            first,
            numberOfElements,
            empty
        });
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Produto produto)
    {
        await _repo.InserirAsync(produto);
        return CreatedAtAction(nameof(Listar), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, [FromBody] Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest(new ErrorDetails
            {
                Timestamp = DateTime.UtcNow,
                Message = "ID do produto não corresponde ao da URL.",
                Details = $"ID esperado: {id}, recebido: {produto.Id}"
            });
        }

        await _repo.AtualizarAsync(produto);
        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(string id)
    {
        await _repo.DeletarAsync(id);
        return NoContent();
    }
}
