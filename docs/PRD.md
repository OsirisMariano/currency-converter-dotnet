# PRD — Conversor de Moedas Pro (v2.0)

> Product Requirements Document — Última atualização: 2026-08-17

---

## 1. Visão Geral

Transformar o conversor de moedas educacional em uma **aplicação console profissional** com interface terminal rica, capaz de converter entre 5-6 moedas principais usando taxas de câmbio em tempo real, com persistência de histórico via SQLite.

## 2. Público-alvo

Usuários gerais que precisam de conversões rápidas e confiáveis no terminal.

## 3. Stack Tecnológica

| Camada | Tecnologia |
|--------|-----------|
| Runtime | .NET 10 (Console App) |
| UI | **Spectre.Console** (tabelas, menus, cores, prompts) |
| API | ExchangeRate-API (gratuita, sem API key) |
| HTTP Client | `HttpClient` nativo + `System.Text.Json` |
| Persistência | **SQLite** via `Microsoft.Data.Sqlite` |
| Testes | xUnit + Moq |
| Container | Docker multi-stage + docker-compose |
| CI/CD | GitHub Actions (build + test) |

## 4. Funcionalidades

### 4.1 Conversão de Moedas
- Suporte a **USD, EUR, GBP, JPY, ARS** (e BRL como base)
- Taxas em tempo real via API (com cache de 1h para reduzir chamadas)
- Fallback para taxa fixa se API falhar

### 4.2 Interface Terminal Rica (Spectre.Console)
- Menu principal navegável com setas
- Tabelas coloridas com resultado da conversão
- Prompts validados (sem erros de entrada)
- Barra de progresso para chamadas API
- Output formatado com símbolos de moeda

### 4.3 Histórico de Conversões
- Salva cada conversão no SQLite (data, moedas, valor, resultado)
- Consulta das últimas N conversões
- Opção de limpar histórico

### 4.4 Persistência
- SQLite local em volume Docker (`/app/data/conversor.db`)
- Tabelas: `conversoes`, `configuracoes`
- Configurações: moeda preferida, tema

### 4.5 Testes Automatizados
- Testes unitários: cálculos, validação de entrada, services
- Testes de integração: repositório SQLite, chamadas API (mockadas)
- Cobertura mínima: 80%

### 4.6 Docker/Deploy
- `Dockerfile` multi-stage (build + runtime)
- `Dockerfile.dev` para desenvolvimento com hot reload
- `docker-compose.yml` para desenvolvimento
- `docker-compose.prod.yml` para produção
- GitHub Actions: build + test no push/PR

## 5. Estrutura de Pastas

```
currency-converter-dotnet/
├── docker-compose.yml          # Ambiente dev (hot reload)
├── docker-compose.prod.yml     # Ambiente prod
├── Dockerfile                  # Produção (multi-stage)
├── Dockerfile.dev              # Desenvolvimento
├── .dockerignore
├── currency-converter-dotnet.sln
├── docs/
│   └── PRD.md
├── src/
│   └── CurrencyConverter/
│       ├── CurrencyConverter.csproj
│       ├── Program.cs
│       ├── Commands/
│       │   ├── MenuCommand.cs
│       │   ├── ConvertCommand.cs
│       │   └── HistoryCommand.cs
│       ├── Services/
│       │   ├── IExchangeRateService.cs
│       │   ├── ExchangeRateApiService.cs
│       │   ├── FallbackExchangeRateService.cs
│       │   └── CacheService.cs
│       ├── Models/
│       │   ├── Conversion.cs
│       │   ├── Currency.cs
│       │   └── ExchangeRate.cs
│       ├── Data/
│       │   ├── IConversionRepository.cs
│       │   ├── SqliteConversionRepository.cs
│       │   └── DatabaseInitializer.cs
│       └── Configuration/
│           └── AppConfig.cs
├── tests/
│   └── CurrencyConverter.Tests/
│       ├── CurrencyConverter.Tests.csproj
│       ├── Services/
│       │   ├── ExchangeRateApiServiceTests.cs
│       │   └── FallbackExchangeRateServiceTests.cs
│       ├── Data/
│       │   └── SqliteConversionRepositoryTests.cs
│       └── Commands/
│           └── ConvertCommandTests.cs
└── .github/
    └── workflows/
        └── ci.yml
```

## 6. Fases de Entrega (1 mês)

| Semana | Fase | Entregável |
|--------|------|-----------|
| **1** | Fundação | Docker dev, estrutura de pastas, Spectre.Console no menu, HttpClient + chamada API |
| **2** | Core | Conversão multi-moeda funcional, cache de taxas, validação robusta |
| **3** | Persistência + Histórico | SQLite, CRUD de conversões, tela de histórico |
| **4** | Qualidade + Deploy | Testes xUnit (80%+), Dockerfile prod, GitHub Actions, README atualizado |

## 7. Critérios de Aceite (MVP)

- [ ] Conversão entre BRL e pelo menos 5 moedas com taxa real
- [ ] Interface de menu com Spectre.Console (setas, tabelas)
- [ ] Histórico persistido em SQLite e consultável
- [ ] Testes unitários passando com 80%+ cobertura
- [ ] `docker compose up --build` funcional (dev)
- [ ] `docker compose -f docker-compose.prod.yml up --build` funcional (prod)
- [ ] CI verde no GitHub Actions

## 8. Fora de Escopo (v2.0)

- Interface gráfica (WPF/MAUI)
- Autenticação/usuários
- Gráficos de variação histórica
- App mobile
- Container separado para banco de dados

## 9. Decisões Técnicas

| Decisão | Escolha | Justificativa |
|---------|---------|---------------|
| UI | Spectre.Console | Rico, simples, mantém Console App |
| DB | SQLite via ADO.NET | Leve, sem ORM desnecessário, embutido |
| HTTP | HttpClient nativo | Sem dependências extras |
| JSON | System.Text.Json | Nativo do .NET, performático |
| Testes | xUnit + Moq | Padrão da comunidade .NET |
| API | exchangerate-api.com | Gratuita, sem API key |
| Docker | 100% containerizado | Consistência entre ambientes |

## 10. Fluxo de Desenvolvimento

```bash
# Iniciar desenvolvimento (hot reload)
docker compose up --build

# Rodar testes (em outro terminal)
docker compose run tests

# Build de produção
docker compose -f docker-compose.prod.yml up --build

# Limpar
docker compose down -v
```
