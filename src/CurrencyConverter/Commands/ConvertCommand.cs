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

        var currencies = Enum.GetValues<Currency>().ToList();

        var fromTable = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .Title("[bold]Moeda de Origem[/]")
            .AddColumn(new TableColumn("[bold]#[/]").Centered())
            .AddColumn(new TableColumn("[bold]Moeda[/]").Centered())
            .AddColumn(new TableColumn("[bold]Nome[/]").Centered());

        for (int i = 0; i < currencies.Count; i++)
        {
            var c = currencies[i];
            fromTable.AddRow($"[yellow]{i + 1}[/]", $"{c.GetSymbol()} {c}", c.GetName());
        }

        AnsiConsole.Write(fromTable);
        AnsiConsole.WriteLine();

        var fromIndex = AnsiConsole.Prompt(
            new TextPrompt<int>("[bold yellow]Digite o número da moeda de origem:[/]")
                .Validate(val => val >= 1 && val <= currencies.Count
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"Escolha um número entre 1 e {currencies.Count}")));

        Console.Clear();

        AnsiConsole.MarkupLine("[bold blue]💱 Conversão de Moedas[/]");
        AnsiConsole.WriteLine();

        var toTable = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .Title("[bold]Moeda de Destino[/]")
            .AddColumn(new TableColumn("[bold]#[/]").Centered())
            .AddColumn(new TableColumn("[bold]Moeda[/]").Centered())
            .AddColumn(new TableColumn("[bold]Nome[/]").Centered());

        for (int i = 0; i < currencies.Count; i++)
        {
            var c = currencies[i];
            toTable.AddRow($"[yellow]{i + 1}[/]", $"{c.GetSymbol()} {c}", c.GetName());
        }

        AnsiConsole.Write(toTable);
        AnsiConsole.WriteLine();

        var toIndex = AnsiConsole.Prompt(
            new TextPrompt<int>("[bold yellow]Digite o número da moeda de destino:[/]")
                .Validate(val => val >= 1 && val <= currencies.Count
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"Escolha um número entre 1 e {currencies.Count}")));

        var amount = AnsiConsole.Prompt(
            new TextPrompt<double>("[bold yellow]Digite o valor:[/]")
                .Validate(val => val > 0 ? ValidationResult.Success() : ValidationResult.Error("O valor deve ser maior que zero")));

        var from = currencies[fromIndex - 1];
        var to = currencies[toIndex - 1];

        var exchangeRate = await _exchangeRateService.GetRatesAsync(from);
        var rate = exchangeRate.GetRate(from, to);
        var result = amount * rate;

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Green)
            .Title("[bold green]Resultado[/]")
            .AddColumn(new TableColumn("[bold]Detalhe[/]").Centered())
            .AddColumn(new TableColumn("[bold]Valor[/]").Centered())
            .AddRow("Moeda de Origem", $"{from.GetSymbol()} {amount:N2} {from}")
            .AddRow("Moeda de Destino", $"{to.GetSymbol()} {result:N2} {to}")
            .AddRow("Taxa de Câmbio", $"1 {from} = {rate:N4} {to}")
            .AddRow("Data/Hora", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[dim]Pressione Enter para continuar...[/]").AllowEmpty());
    }
}
