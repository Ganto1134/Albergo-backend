using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Albergo.DAO
{
    public class PrenotazioneDAO : IPrenotazioneDAO
    {
        private readonly string _connectionString;

        public PrenotazioneDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreatePrenotazioneAsync(Prenotazione prenotazione)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, Dal, Al, TipoSoggiorno, UserId) " +
                    "VALUES (@CodiceFiscaleCliente, @NumeroCamera, GETDATE(), @Dal, @Al, @TipoSoggiorno, @UserId)", connection);

                command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                command.Parameters.AddWithValue("@Al", prenotazione.Al);
                command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UserId", prenotazione.UserId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Prenotazione> GetPrenotazioneAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Prenotazioni WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Prenotazione
                        {
                            ID = (int)reader["ID"],
                            CodiceFiscaleCliente = reader["CodiceFiscaleCliente"].ToString(),
                            NumeroCamera = (int)reader["NumeroCamera"],
                            DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                            Dal = (DateTime)reader["Dal"],
                            Al = (DateTime)reader["Al"],
                            TipoSoggiorno = reader["TipoSoggiorno"].ToString(),
                            UserId = (int)reader["UserId"]
                        };
                    }
                }
            }
            return null;
        }

        public async Task<List<Prenotazione>> GetPrenotazioniByUserIdAsync(int userId)
        {
            var prenotazioni = new List<Prenotazione>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Prenotazioni WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        prenotazioni.Add(new Prenotazione
                        {
                            ID = (int)reader["ID"],
                            CodiceFiscaleCliente = reader["CodiceFiscaleCliente"].ToString(),
                            NumeroCamera = (int)reader["NumeroCamera"],
                            DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                            Dal = (DateTime)reader["Dal"],
                            Al = (DateTime)reader["Al"],
                            TipoSoggiorno = reader["TipoSoggiorno"].ToString(),
                            UserId = (int)reader["UserId"]
                        });
                    }
                }
            }
            return prenotazioni;
        }

        public async Task UpdatePrenotazioneAsync(Prenotazione prenotazione)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Prenotazioni SET CodiceFiscaleCliente = @CodiceFiscaleCliente, NumeroCamera = @NumeroCamera, Dal = @Dal, Al = @Al, TipoSoggiorno = @TipoSoggiorno WHERE ID = @ID", connection);

                command.Parameters.AddWithValue("@ID", prenotazione.ID);
                command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                command.Parameters.AddWithValue("@Al", prenotazione.Al);
                command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno ?? (object)DBNull.Value);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeletePrenotazioneAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Prenotazioni WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
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
                        camere.Add(new Camera
                        {
                            Numero = (int)reader["Numero"],
                            Descrizione = reader["Descrizione"].ToString(),
                            Tipologia = reader["Tipologia"].ToString(),
                            CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                            TariffaApplicata = (decimal)reader["TariffaApplicata"]
                        });
                    }
                }
            }
            return camere;
        }
    }
}
