using TrocaMoedas.Domain.Models;

namespace TrocaMoedas.Application.Repositories;

public interface IConversionRepository
{
    Task SaveAsync(Conversion conversion);
    Task<List<Conversion>> GetRecentAsync(int count);
    Task ClearAsync();
}
