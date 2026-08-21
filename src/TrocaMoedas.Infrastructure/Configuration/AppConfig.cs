using Microsoft.Data.Sqlite;

namespace TrocaMoedas.Infrastructure.Configuration;

public class AppConfig
{
    private readonly string _connectionString;

    public AppConfig(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<string?> GetAsync(string key)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = "SELECT value FROM configuracoes WHERE key = @Key";
        await using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Key", key);

        var result = await cmd.ExecuteScalarAsync();
        return result?.ToString();
    }

    public async Task SetAsync(string key, string value)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            INSERT OR REPLACE INTO configuracoes (key, value)
            VALUES (@Key, @Value)";
        await using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Key", key);
        cmd.Parameters.AddWithValue("@Value", value);

        await cmd.ExecuteNonQueryAsync();
    }
}
