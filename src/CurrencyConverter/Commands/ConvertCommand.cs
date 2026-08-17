using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Spectre.Console;

namespace CurrencyConverter.Commands;

public class ConvertCommand
{
    private readonly IExchangeRateService _exchangeRateService;

    public ConvertCommand(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    public async Task RunAsync()
    {
        Console.Clear();

        AnsiConsole.MarkupLine("[bold blue]💱 Conversão de Moedas[/]");
        AnsiConsole.WriteLine();

        var fromCurrency = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]Moeda de origem:[/]")
                .HighlightStyle(new Style(Color.Yellow))
                .AddChoices(Enum.GetNames<Currency>().Select(c => $"{GetSymbol(c)} {c} - {GetName(c)}")));

        var toCurrency = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]Moeda de destino:[/]")
                .HighlightStyle(new Style(Color.Yellow))
                .AddChoices(Enum.GetNames<Currency>().Select(c => $"{GetSymbol(c)} {c} - {GetName(c)}")));

        var amount = AnsiConsole.Prompt(
            new TextPrompt<double>("[bold yellow]Valor:[/]")
                .Validate(val => val > 0 ? ValidationResult.Success() : ValidationResult.Error("O valor deve ser maior que zero")));

        var from = Enum.Parse<Currency>(fromCurrency.Split(' ')[1]);
        var to = Enum.Parse<Currency>(toCurrency.Split(' ')[1]);

        var exchangeRate = await _exchangeRateService.GetRatesAsync(from);
        var rate = exchangeRate.GetRate(from, to);
        var result = amount * rate;

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .AddColumn(new TableColumn("[bold]Detalhe[/]").Centered())
            .AddColumn(new TableColumn("[bold]Valor[/]").Centered())
            .AddRow("Moeda de Origem", $"{GetSymbol(from)} {amount:N2} {from}")
            .AddRow("Moeda de Destino", $"{GetSymbol(to)} {result:N2} {to}")
            .AddRow("Taxa de Câmbio", $"1 {from} = {rate:N4} {to}")
            .AddRow("Data/Hora", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Pressione qualquer tecla para continuar...[/]");
        Console.ReadKey(true);
    }

    private static string GetSymbol(Currency currency) => currency switch
    {
        Currency.BRL => "R$",
        Currency.USD => "$",
        Currency.EUR => "€",
        Currency.GBP => "£",
        Currency.JPY => "¥",
        Currency.ARS => "$",
        _ => currency.ToString()
    };

    private static string GetName(Currency currency) => currency switch
    {
        Currency.BRL => "Real Brasileiro",
        Currency.USD => "Dólar Americano",
        Currency.EUR => "Euro",
        Currency.GBP => "Libra Esterlina",
        Currency.JPY => "Iene Japonês",
        Currency.ARS => "Peso Argentino",
        _ => currency.ToString()
    };
}
