using Microsoft.Data.Sqlite;

namespace TrocaMoedas.Infrastructure.Data;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeAsync()
    {
        var dbPath = ExtractDbPath(_connectionString);
        if (!string.IsNullOrEmpty(dbPath))
        {
            var directory = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var createTableConversoes = @"
            CREATE TABLE IF NOT EXISTS conversoes (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                from_currency TEXT NOT NULL,
                to_currency TEXT NOT NULL,
                amount REAL NOT NULL,
                result REAL NOT NULL,
                rate REAL NOT NULL,
                created_at TEXT NOT NULL
            )";
        await using var cmd1 = new SqliteCommand(createTableConversoes, connection);
        await cmd1.ExecuteNonQueryAsync();

        var createTableConfiguracoes = @"
            CREATE TABLE IF NOT EXISTS configuracoes (
                key TEXT PRIMARY KEY,
                value TEXT NOT NULL
            )";
        await using var cmd2 = new SqliteCommand(createTableConfiguracoes, connection);
        await cmd2.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }

    private static string ExtractDbPath(string connectionString)
    {
        var parts = connectionString.Split(';');
        foreach (var part in parts)
        {
            if (part.Trim().StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase))
            {
                return part.Split('=')[1].Trim();
            }
        }
        return string.Empty;
    }
}
