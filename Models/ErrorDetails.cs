namespace EcommerceApi.Models;

public class ErrorDetails
{
    public DateTime Timestamp { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}