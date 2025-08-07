namespace EcommerceApi.Models;

public class Produto
{
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public double Price { get; set; }
}