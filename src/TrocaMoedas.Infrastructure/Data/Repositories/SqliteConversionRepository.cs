using Microsoft.Data.Sqlite;
using TrocaMoedas.Application.Repositories;
using TrocaMoedas.Domain.Models;

namespace TrocaMoedas.Infrastructure.Data.Repositories;

public class SqliteConversionRepository : IConversionRepository
{
    private readonly string _connectionString;

    public SqliteConversionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task SaveAsync(Conversion conversion)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            INSERT INTO conversoes (from_currency, to_currency, amount, result, rate, created_at)
            VALUES (@FromCurrency, @ToCurrency, @Amount, @Result, @Rate, @CreatedAt)";

        await using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@FromCurrency", conversion.FromCurrency.ToString());
        cmd.Parameters.AddWithValue("@ToCurrency", conversion.ToCurrency.ToString());
        cmd.Parameters.AddWithValue("@Amount", conversion.Amount);
        cmd.Parameters.AddWithValue("@Result", conversion.Result);
        cmd.Parameters.AddWithValue("@Rate", conversion.Rate);
        cmd.Parameters.AddWithValue("@CreatedAt", conversion.CreatedAt);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<Conversion>> GetRecentAsync(int count)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = "SELECT * FROM conversoes ORDER BY created_at DESC LIMIT @Count";
        await using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Count", count);

        var conversions = new List<Conversion>();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            conversions.Add(new Conversion
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                FromCurrency = Enum.Parse<Currency>(reader.GetString(reader.GetOrdinal("from_currency"))),
                ToCurrency = Enum.Parse<Currency>(reader.GetString(reader.GetOrdinal("to_currency"))),
                Amount = reader.GetDouble(reader.GetOrdinal("amount")),
                Result = reader.GetDouble(reader.GetOrdinal("result")),
                Rate = reader.GetDouble(reader.GetOrdinal("rate")),
                CreatedAt = DateTime.Parse(reader.GetString(reader.GetOrdinal("created_at")))
            });
        }

        return conversions;
    }

    public async Task ClearAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = "DELETE FROM conversoes";
        await using var cmd = new SqliteCommand(sql, connection);
        await cmd.ExecuteNonQueryAsync();
    }
}
