using Spectre.Console;

namespace CurrencyConverter.Commands;

public class MenuCommand
{
    private readonly ConvertCommand _convertCommand;

    public MenuCommand(ConvertCommand convertCommand)
    {
        _convertCommand = convertCommand;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();

            AnsiConsole.Write(new FigletText("Conversor de Moedas").Color(Color.Blue));
            AnsiConsole.WriteLine();

            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Yellow)
                .AddColumn(new TableColumn("[bold]#[/]").Centered())
                .AddColumn(new TableColumn("[bold]Opção[/]").Centered())
                .AddRow("[yellow]1[/]", "💱 Converter Moeda")
                .AddRow("[yellow]2[/]", "📊 Histórico de Conversões")
                .AddRow("[yellow]3[/]", "⚙️  Configurações")
                .AddRow("[yellow]4[/]", "🚪 Sair");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();

            var index = AnsiConsole.Prompt(
                new TextPrompt<int>("[bold yellow]Digite o número da opção:[/]")
                    .Validate(val => val >= 1 && val <= 4
                        ? ValidationResult.Success()
                        : ValidationResult.Error("Escolha um número entre 1 e 4")));

            switch (index)
            {
                case 1:
                    await _convertCommand.RunAsync();
                    break;
                case 2:
                    AnsiConsole.MarkupLine("[yellow]Funcionalidade será implementada no Passo 3[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.Prompt(new TextPrompt<string>("[dim]Pressione Enter para continuar...[/]").AllowEmpty());
                    break;
                case 3:
                    AnsiConsole.MarkupLine("[yellow]Funcionalidade será implementada no Passo 3[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.Prompt(new TextPrompt<string>("[dim]Pressione Enter para continuar...[/]").AllowEmpty());
                    break;
                case 4:
                    AnsiConsole.MarkupLine("[green]Obrigado por usar o Conversor de Moedas![/]");
                    return;
            }
        }
    }
}
