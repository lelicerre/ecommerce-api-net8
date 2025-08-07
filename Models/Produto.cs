namespace EcommerceApi.Models;

public class Produto
{
    public string Id { get; set; } = string.Empty;
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public string CodigoDepartamento { get; set; }
    public decimal Preco { get; set; }
    public bool Status { get; set; }
    public bool Deletado { get; set; }
}