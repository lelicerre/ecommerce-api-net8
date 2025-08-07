using System.Text.Json.Serialization;

namespace EcommerceApi.Models;

public class Departamento
{
    [JsonPropertyName("code")]
    public string Codigo { get; set; }

    [JsonPropertyName("description")]
    public string Descricao { get; set; }
}