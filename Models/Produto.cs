using System.Text.Json.Serialization;

namespace EcommerceApi.Models;

public class Produto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Codigo { get; set; }

    [JsonPropertyName("description")]
    public string Descricao { get; set; }

    [JsonPropertyName("department")]
    public string CodigoDepartamento { get; set; }

    [JsonPropertyName("price")]
    public decimal Preco { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("deletado")]
    public bool Deletado { get; set; }
}