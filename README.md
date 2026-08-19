# Troca Moedas

Conversão de moedas direto do terminal — rápido, prático e containerizado.

## Sobre

Ferramenta de linha de comando para converter valores entre diferentes moedas com taxas de câmbio atualizadas. Interface interativa, validação de entrada e total suporte a Docker.

## Moedas Suportadas

| Moeda | Código | Nome             |
|-------|--------|------------------|
| R$    | BRL    | Real Brasileiro  |
| $     | USD    | Dólar Americano  |
| €     | EUR    | Euro             |
| £     | GBP    | Libra Esterlina  |
| ¥     | JPY    | Iene Japonês     |
| $     | ARS    | Peso Argentino   |

## Funcionalidades

- Taxas de câmbio em tempo real via API, com fallback automático
- Interface de terminal com tabelas, cores e menus navegáveis
- Validação de entrada em todas as etapas
- Suporte a conversão entre quaisquer duas moedas da lista
- Ambiente 100% containerizado com Docker

## Stack Tecnológica

| Camada        | Tecnologia                          |
|---------------|--------------------------------------|
| Runtime       | .NET 10                              |
| Linguagem     | C#                                   |
| UI Terminal   | Spectre.Console                      |
| API de Câmbio | ExchangeRate-API                     |
| Container     | Docker + docker-compose              |

## Como Rodar

### Com Docker (recomendado)

```bash
docker compose up --build
```

### Sem Docker

```bash
dotnet run --project src/TrocaMoedas
```

### Variável de Ambiente (opcional)

Para usar taxas em tempo real, configure sua API key:

```bash
export EXCHANGE_RATE_API_KEY=sua-chave-aqui
```

> Se não configurada, o sistema usa taxas de fallback automaticamente.

## Estrutura do Projeto

```
troca-moedas/
├── Dockerfile.dev
├── docker-compose.yml
├── src/TrocaMoedas/
│   ├── Program.cs
│   ├── Commands/
│   │   ├── MenuCommand.cs
│   │   └── ConvertCommand.cs
│   ├── Services/
│   │   ├── IExchangeRateService.cs
│   │   ├── ExchangeRateApiService.cs
│   │   └── FallbackExchangeRateService.cs
│   └── Models/
│       ├── Currency.cs
│       ├── Conversion.cs
│       └── ExchangeRate.cs
└── docs/
    └── PRD.md
```

## Licença

Projeto open source para fins educacionais e profissionais.
