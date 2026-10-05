using Dapper;
using Microsoft.Data.Sqlite;
using Parcial1_P4_Winiffer.Modelos;

namespace Parcial1_P4_Winiffer.Services
{
    public class NumbersService(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("SqliteConnection")!;

        private SqliteConnection createConection => new SqliteConnection(_connectionString);

        public async Task InitializeAsync()
        {

            const string consulta = @"
                    CREATE TABLE IF NOT EXISTS Numbers(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Fecha TEXT NOT NULL,
                        Numero REAL NOT NULL,
                        Resultado REAL NOT NULL
                    );
                    ";


            using var connection = createConection;
            await connection.ExecuteAsync(consulta);

        }


        public async Task<int> SaveAsync(NumberRecord number)
        {

            const string consulta = @"
                    INSERT INTO Numbers (Fecha, Numero, Resultado)
                    VALUES (@Fecha, @Numero, @Resultado);
                    SELECT last_insert_rowid();
                    ";
            using var connection = createConection;
            return await connection.ExecuteScalarAsync<int>(consulta, number);

        }

        public async Task<bool> UpdateAsync(NumberRecord number)
        {
            const string consulta = @"
                    UPDATE Numbers
                    SET Fecha = @Fecha,
                        Numero = @Numero,
                        Resultado = @Resultado
                    WHERE Id = @Id;
                    ";
            using var connection = createConection;
            int rowsAffected = await connection.ExecuteAsync(consulta, number);
            return rowsAffected > 0;
        }


        public async Task<NumberRecord?> GetByIdAsync(int id)
        {
            const string consulta = @"
                    SELECT Id, Fecha, Numero, Resultado
                    FROM Numbers
                    WHERE Id = @Id;
                    ";
            using var connection = createConection;
            return await connection.QuerySingleOrDefaultAsync<NumberRecord>(consulta, new { Id = id });

        }


        public async Task<IEnumerable<NumberRecord>> GetListAsync()
        {
            const string consulta = """
            SELECT Id, Fecha, Numero, Resultado
            FROM Numbers
            ORDER BY Id DESC;
            """;

            using var connection = createConection;
            return await connection.QueryAsync<NumberRecord>(consulta);

        }


    }

}