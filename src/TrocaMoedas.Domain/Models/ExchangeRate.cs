namespace TrocaMoedas.Domain.Models;

public record ExchangeRate
{
    public Currency BaseCurrency { get; init; }
    public Dictionary<Currency, double> Rates { get; init; } = new();
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    public double GetRate(Currency from, Currency to)
    {
        if (from == to) return 1.0;

        if (from == BaseCurrency && Rates.TryGetValue(to, out var rate))
            return rate;

        if (to == BaseCurrency && Rates.TryGetValue(from, out var inverseRate))
            return 1.0 / inverseRate;

        if (Rates.TryGetValue(from, out var fromRate) && Rates.TryGetValue(to, out var toRate))
            return toRate / fromRate;

        throw new InvalidOperationException($"Taxa de câmbio não encontrada para {from} → {to}");
    }
}
