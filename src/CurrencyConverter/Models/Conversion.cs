namespace CurrencyConverter.Models;

public record Conversion
{
    public int Id { get; init; }
    public Currency FromCurrency { get; init; }
    public Currency ToCurrency { get; init; }
    public double Amount { get; init; }
    public double Result { get; init; }
    public double Rate { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
