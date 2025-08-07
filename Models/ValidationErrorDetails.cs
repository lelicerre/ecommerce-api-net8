namespace EcommerceApi.Models;

public class ValidationErrorDetails : ErrorDetails
{
    public Dictionary<string, string> ValidationErrors { get; set; } = new();
}