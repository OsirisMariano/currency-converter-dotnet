using TrocaMoedas.Commands;
using TrocaMoedas.Services;
using Spectre.Console;

namespace TrocaMoedas;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var httpClient = new HttpClient();
        var apiKey = Environment.GetEnvironmentVariable("EXCHANGE_RATE_API_KEY") ?? "";

        IExchangeRateService exchangeRateService;

        if (string.IsNullOrEmpty(apiKey))
        {
            AnsiConsole.MarkupLine("[yellow]⚠️  API key não configurada. Usando taxas de fallback.[/]");
            AnsiConsole.MarkupLine("[dim]Configure EXCHANGE_RATE_API_KEY para usar taxas em tempo real.[/]");
            AnsiConsole.WriteLine();
            exchangeRateService = new FallbackExchangeRateService();
        }
        else
        {
            exchangeRateService = new ExchangeRateApiService(httpClient, apiKey);
        }

        var convertCommand = new ConvertCommand(exchangeRateService);
        var menuCommand = new MenuCommand(convertCommand);

        await menuCommand.RunAsync();
    }
}
