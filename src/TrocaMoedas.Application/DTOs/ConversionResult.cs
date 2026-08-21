using TrocaMoedas.Domain.Models;

namespace TrocaMoedas.Application.DTOs;

public record ConversionResult(
    Currency From,
    Currency To,
    double Amount,
    double Result,
    double Rate
);
