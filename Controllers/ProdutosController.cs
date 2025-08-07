using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Models;
using EcommerceApi.Repositories;

namespace EcommerceApi.Controllers;

/// <summary>
/// Controller responsável pelo CRUD de produtos.
/// </summary>
[ApiController]
[Route("produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoRepository _repo;

    public ProdutosController(ProdutoRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Lista todos os produtos com paginação.
    /// </summary>
    /// <param name="page">Número da página (começa em 0)</param>
    /// <param name="size">Tamanho da página</param>
    /// <returns>Lista paginada de produtos</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="produto">Objeto do produto</param>
    /// <returns>Produto criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar([FromBody] Produto produto)
    {
        await _repo.InserirAsync(produto);
        return CreatedAtAction(nameof(Listar), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Atualiza um produto existente.
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <param name="produto">Dados atualizados</param>
    /// <returns>Produto atualizado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Realiza a exclusão lógica de um produto.
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Status 204 se sucesso</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar(string id)
    {
        await _repo.DeletarAsync(id);
        return NoContent();
    }
}
