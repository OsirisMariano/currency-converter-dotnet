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

        var currencyChoices = Enum.GetValues<Currency>()
            .ToDictionary(c => c.GetDisplayName(), c => c);

        var fromDisplay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]Moeda de origem:[/]")
                .HighlightStyle(new Style(Color.Yellow))
                .AddChoices(currencyChoices.Keys));

        var toDisplay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]Moeda de destino:[/]")
                .HighlightStyle(new Style(Color.Yellow))
                .AddChoices(currencyChoices.Keys));

        var amount = AnsiConsole.Prompt(
            new TextPrompt<double>("[bold yellow]Valor:[/]")
                .Validate(val => val > 0 ? ValidationResult.Success() : ValidationResult.Error("O valor deve ser maior que zero")));

        var from = currencyChoices[fromDisplay];
        var to = currencyChoices[toDisplay];

        var exchangeRate = await _exchangeRateService.GetRatesAsync(from);
        var rate = exchangeRate.GetRate(from, to);
        var result = amount * rate;

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .AddColumn(new TableColumn("[bold]Detalhe[/]").Centered())
            .AddColumn(new TableColumn("[bold]Valor[/]").Centered())
            .AddRow("Moeda de Origem", $"{from.GetSymbol()} {amount:N2} {from}")
            .AddRow("Moeda de Destino", $"{to.GetSymbol()} {result:N2} {to}")
            .AddRow("Taxa de Câmbio", $"1 {from} = {rate:N4} {to}")
            .AddRow("Data/Hora", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Pressione qualquer tecla para continuar...[/]");
        Console.ReadKey(true);
    }
}
