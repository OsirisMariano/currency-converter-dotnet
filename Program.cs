using System;
using System.Globalization;

namespace ConversorMoedas
{
  class Program
  {
    static void Main(string[] args)
    {
      double taxaDolar = 5.00;
      bool entradaValida = false;
      double valorReais = 0;

      Console.WriteLine("== Conversor de Moedas Pro (v1.1) ==");

      // Usamos um loop 'while' para insistir até que o usuário digite um número correto
      while (!entradaValida)
      {
        Console.Write("\nDigite o valor em Reais (R$): ");
        string? input = Console.ReadLine();

        // O TryParse tenta converter. Se conseguir, retorna 'true' e guarda em 'valorReais'
        if (double.TryParse(input, NumberStyles.Any, CultureInfo.GetCultureInfo("pt-BR"), out valorReais))
        {
          if (valorReais >= 0)
          {
            entradaValida = true;
          }
          else
          {
            Console.WriteLine("[ERRO] Por favor, digite um valor positivo.");
          }
        }
        else
        {
          Console.WriteLine("[ERRO] Entrada inválida! Digite apenas números (ex: 10,50).");
        }
      }

      // Processamento
      double valorDolar = valorReais / taxaDolar;

      // Saída
      Console.WriteLine("\n -- Resultado --");
      Console.WriteLine($"Valor em Reais: {valorReais.ToString("C", CultureInfo.GetCultureInfo("pt-BR"))}");
      Console.WriteLine($"Valor em Dólar: {valorDolar.ToString("C", CultureInfo.GetCultureInfo("en-US"))}");

      Console.WriteLine("\nPressione qualquer tecla para sair...");
      Console.ReadKey();
    }
  }
}