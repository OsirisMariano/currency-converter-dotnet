using System;
using System.Globalization;

namespace ConversorMoedas
{
  class Program
  {
    static void Main(string[] args)
    {
      // 1. Configuração da Taxa e Variáveis
      double taxaDolar = 5.00;

      Console.WriteLine("== Conversor de Moedas - Real para Dólar ==");

      Console.Write("Digite o valor em Reais (R$): ");
      string? input = Console.ReadLine();
      double ValorReais = double.Parse(input ?? "0");
      double ValorDolar = ValorReais / taxaDolar;

      Console.WriteLine("\n -- Resultado --");
      Console.WriteLine($"Valor em Reais: {ValorReais.ToString("C", CultureInfo.GetCultureInfo("pt-BR"))}");
      Console.WriteLine($"Valor em Dólar: {ValorDolar.ToString("C", CultureInfo.GetCultureInfo("en-US"))}");

      Console.WriteLine("\nPressione qualquer tecla para sair...");
      Console.ReadKey();

    }
  }
}