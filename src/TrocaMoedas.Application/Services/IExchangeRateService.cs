using TrocaMoedas.Domain.Models;

namespace TrocaMoedas.Application.Services;

public interface IExchangeRateService
{
    Task<ExchangeRate> GetRatesAsync(Currency baseCurrency);
    double Convert(Currency from, Currency to, double amount);
}
