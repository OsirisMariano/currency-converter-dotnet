using CurrencyConverter.Models;

namespace CurrencyConverter.Services;

public interface IExchangeRateService
{
    Task<ExchangeRate> GetRatesAsync(Currency baseCurrency);
    double Convert(Currency from, Currency to, double amount);
}
