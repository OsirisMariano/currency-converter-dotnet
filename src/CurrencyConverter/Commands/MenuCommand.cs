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

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Selecione uma opção:[/]")
                    .HighlightStyle(new Style(Color.Yellow))
                    .AddChoices(new[]
                    {
                        "💱 Converter Moeda",
                        "📊 Histórico de Conversões",
                        "⚙️  Configurações",
                        "🚪 Sair"
                    }));

            switch (choice)
            {
                case "💱 Converter Moeda":
                    await _convertCommand.RunAsync();
                    break;
                case "📊 Histórico de Conversões":
                    AnsiConsole.MarkupLine("[yellow]Funcionalidade será implementada no Passo 3[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("[dim]Pressione qualquer tecla para continuar...[/]");
                    Console.ReadKey(true);
                    break;
                case "⚙️  Configurações":
                    AnsiConsole.MarkupLine("[yellow]Funcionalidade será implementada no Passo 3[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("[dim]Pressione qualquer tecla para continuar...[/]");
                    Console.ReadKey(true);
                    break;
                case "🚪 Sair":
                    AnsiConsole.MarkupLine("[green]Obrigado por usar o Conversor de Moedas![/]");
                    return;
            }
        }
    }
}
