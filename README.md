# 💰 Conversor de Moedas (Console Application)

> Projeto desenvolvido para fins de aprendizado sobre lógica de programação, 
> tipos de dados e manipulação de entrada/saída com .NET e C#.

## 🎯 Objetivo do Projeto
Automatizar a tarefa cotidiana de converter valores entre diferentes moedas (inicialmente Real para Dólar), permitindo que o usuário obtenha resultados rápidos através de uma interface de linha de comando (CLI).

## 🛠️ Tecnologias Utilizadas
* **Linguagem:** C# (C-Sharp).
* **Plataforma:** .NET (Multiplataforma).
* **IDE Sugerida:** Visual Studio 2022 ou Visual Studio Code.

## 📋 Funcionalidades
- [x] Entrada de valor em Reais (BRL) via teclado.
- [x] Processamento matemático com taxa de câmbio atualizável.
- [x] Exibição do valor convertido em Dólares (USD) com formatação decimal.

## 🚀 Como Executar o Projeto

1. **Pré-requisitos:**
   - Possuir o [.NET SDK](https://dotnet.microsoft.com/download) instalado em sua máquina.

2. **Clonagem ou Criação:**
   - Clone este repositório ou crie um novo projeto do tipo `Console App` no seu ambiente.

3. **Execução:**
   - No terminal, dentro da pasta do projeto, digite:
     ```bash
     dotnet run
     ```

## 🏗️ Estrutura de Código (Lógica)
O projeto segue uma execução estruturada (de cima para baixo):
1. **Entrada:** O programa solicita os dados ao usuário através do `Console.ReadLine()`.
2. **Processamento:** Converte o texto recebido em um tipo numérico (`float` ou `double`) e realiza o cálculo.
3. **Saída:** Exibe o resultado utilizando o `Console.WriteLine()`.

## 📝 Licença
Este projeto é para fins educacionais e segue o modelo de código aberto.