using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Albergo.Models;

namespace Albergo.DAO
{
    public class CameraDAO : ICameraDAO
    {
        private readonly string _connectionString;

        public CameraDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Camera>> GetAllCamereAsync()
        {
            var camere = new List<Camera>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Camere", connection);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var camera = new Camera
                        {
                            Numero = (int)reader["Numero"],
                            Descrizione = reader["Descrizione"].ToString(),
                            Tipologia = reader["Tipologia"].ToString(),
                            CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                            TariffaApplicata = (decimal)reader["TariffaApplicata"]
                        };
                        camere.Add(camera);
                    }
                }
            }

            return camere;
        }

        public async Task<Camera> GetCameraByIdAsync(int id)
        {
            Camera camera = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Camere WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", id);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        camera = new Camera
                        {
                            Numero = (int)reader["Numero"],
                            Descrizione = reader["Descrizione"].ToString(),
                            Tipologia = reader["Tipologia"].ToString(),
                            CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                            TariffaApplicata = (decimal)reader["TariffaApplicata"]
                        };
                    }
                }
            }

            return camera;
        }

        public async Task AddCameraAsync(Camera camera)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO Camere (Numero, Descrizione, Tipologia, CaparraConfirmatoria, TariffaApplicata) VALUES (@Numero, @Descrizione, @Tipologia, @CaparraConfirmatoria, @TariffaApplicata)",
                    connection);

                command.Parameters.AddWithValue("@Numero", camera.Numero);
                command.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                command.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                command.Parameters.AddWithValue("@CaparraConfirmatoria", camera.CaparraConfirmatoria);
                command.Parameters.AddWithValue("@TariffaApplicata", camera.TariffaApplicata);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateCameraAsync(Camera camera)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Camere SET Descrizione = @Descrizione, Tipologia = @Tipologia, CaparraConfirmatoria = @CaparraConfirmatoria, TariffaApplicata = @TariffaApplicata WHERE Numero = @Numero",
                    connection);

                command.Parameters.AddWithValue("@Numero", camera.Numero);
                command.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                command.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                command.Parameters.AddWithValue("@CaparraConfirmatoria", camera.CaparraConfirmatoria);
                command.Parameters.AddWithValue("@TariffaApplicata", camera.TariffaApplicata);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteCameraAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Camere WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
