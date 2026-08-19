namespace TrocaMoedas.Models;

public enum Currency
{
    BRL,
    USD,
    EUR,
    GBP,
    JPY,
    ARS
}

public static class CurrencyExtensions
{
    private static readonly Dictionary<Currency, (string Symbol, string Name)> CurrencyInfo = new()
    {
        [Currency.BRL] = ("R$", "Real Brasileiro"),
        [Currency.USD] = ("$", "Dólar Americano"),
        [Currency.EUR] = ("€", "Euro"),
        [Currency.GBP] = ("£", "Libra Esterlina"),
        [Currency.JPY] = ("¥", "Iene Japonês"),
        [Currency.ARS] = ("$", "Peso Argentino")
    };

    public static string GetSymbol(this Currency currency) =>
        CurrencyInfo.TryGetValue(currency, out var info) ? info.Symbol : currency.ToString();

    public static string GetName(this Currency currency) =>
        CurrencyInfo.TryGetValue(currency, out var info) ? info.Name : currency.ToString();

    public static string GetDisplayName(this Currency currency) =>
        $"{currency.GetSymbol()} {currency} - {currency.GetName()}";
}
