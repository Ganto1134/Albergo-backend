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

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(
                            "INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivoAnno, Anno, Dal, Al, TipoSoggiorno, UserId) " +
                            "VALUES (@CodiceFiscaleCliente, @NumeroCamera, @DataPrenotazione, @NumeroProgressivoAnno, @Anno, @Dal, @Al, @TipoSoggiorno, @UserId)", connection, transaction);

                        command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                        command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                        command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                        command.Parameters.AddWithValue("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno);
                        command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                        command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                        command.Parameters.AddWithValue("@Al", prenotazione.Al);
                        command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno);
                        command.Parameters.AddWithValue("@UserId", prenotazione.UserId);

                        await command.ExecuteNonQueryAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
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
                            NumeroProgressivoAnno = (int)reader["NumeroProgressivoAnno"],
                            Anno = (int)reader["Anno"],
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

        public async Task<List<Prenotazione>> GetAllPrenotazioniAsync()
        {
            var prenotazioni = new List<Prenotazione>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Prenotazioni", connection);

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
                            NumeroProgressivoAnno = (int)reader["NumeroProgressivoAnno"],
                            Anno = (int)reader["Anno"],
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
                    "UPDATE Prenotazioni SET CodiceFiscaleCliente = @CodiceFiscaleCliente, NumeroCamera = @NumeroCamera, DataPrenotazione = @DataPrenotazione, Dal = @Dal, Al = @Al, TipoSoggiorno = @TipoSoggiorno, UserId = @UserId WHERE ID = @ID", connection);

                command.Parameters.AddWithValue("@ID", prenotazione.ID);
                command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                command.Parameters.AddWithValue("@Al", prenotazione.Al);
                command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno);
                command.Parameters.AddWithValue("@UserId", prenotazione.UserId);

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

        public async Task<List<Prenotazione>> GetPrenotazioniByCodiceFiscaleAsync(string codiceFiscale)
        {
            var prenotazioni = new List<Prenotazione>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Prenotazioni WHERE CodiceFiscaleCliente = @CodiceFiscaleCliente", connection);
                command.Parameters.AddWithValue("@CodiceFiscaleCliente", codiceFiscale);

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
                            NumeroProgressivoAnno = (int)reader["NumeroProgressivoAnno"],
                            Anno = (int)reader["Anno"],
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

        public async Task<List<ServizioAggiuntivo>> GetServiziAggiuntiviAsync()
        {
            var servizi = new List<ServizioAggiuntivo>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM ServiziAggiuntivi", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var servizio = new ServizioAggiuntivo
                        {
                            ID = (int)reader["ID"],
                            NomeServizio = reader["NomeServizio"].ToString(),
                            Descrizione = reader["Descrizione"].ToString(),
                            Prezzo = (decimal)reader["Prezzo"]
                        };
                        servizi.Add(servizio);
                    }
                }
            }

            return servizi;
        }

        public async Task AggiungiServizioAsync(ServizioPrenotazione servizioPrenotazione)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO ServiziPrenotazione (IDPrenotazione, IDServizio, Data, Quantita, Prezzo) " +
                    "VALUES (@IDPrenotazione, @IDServizio, @Data, @Quantita, @Prezzo)", connection);
                command.Parameters.AddWithValue("@IDPrenotazione", servizioPrenotazione.IDPrenotazione);
                command.Parameters.AddWithValue("@IDServizio", servizioPrenotazione.IDServizio);
                command.Parameters.AddWithValue("@Data", servizioPrenotazione.Data);
                command.Parameters.AddWithValue("@Quantita", servizioPrenotazione.Quantita);
                command.Parameters.AddWithValue("@Prezzo", servizioPrenotazione.Prezzo);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
