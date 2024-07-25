using Albergo.Models;
using System.Data.SqlClient;
using Albergo.DAO;


public class PrenotazioneDAO : IPrenotazioneDAO
{
    private readonly string _connectionString;

    public PrenotazioneDAO(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreatePrenotazioneAsync(Prenotazione prenotazione)
    {
        prenotazione.Anno = DateTime.Now.Year;
        prenotazione.DataPrenotazione = DateTime.Now;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(
                        "SELECT ISNULL(MAX(NumeroProgressivoAnno), 0) FROM Prenotazioni WHERE Anno = @Anno", connection, transaction);
                    command.Parameters.AddWithValue("@Anno", prenotazione.Anno);

                    prenotazione.NumeroProgressivoAnno = (int)await command.ExecuteScalarAsync() + 1;

                    command = new SqlCommand(
                        "INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivoAnno, Anno, Dal, Al, CaparraConfirmatoria, TariffaApplicata, TipoSoggiorno) " +
                        "VALUES (@CodiceFiscaleCliente, @NumeroCamera, @DataPrenotazione, @NumeroProgressivoAnno, @Anno, @Dal, @Al, @CaparraConfirmatoria, @TariffaApplicata, @TipoSoggiorno)", connection, transaction);
                    command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                    command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                    command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                    command.Parameters.AddWithValue("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno);
                    command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                    command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                    command.Parameters.AddWithValue("@Al", prenotazione.Al);
                    command.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
                    command.Parameters.AddWithValue("@TariffaApplicata", prenotazione.TariffaApplicata);
                    command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno);

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
                        CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                        TariffaApplicata = (decimal)reader["TariffaApplicata"],
                        TipoSoggiorno = reader["TipoSoggiorno"].ToString()
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
                        CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                        TariffaApplicata = (decimal)reader["TariffaApplicata"],
                        TipoSoggiorno = reader["TipoSoggiorno"].ToString()
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
                "UPDATE Prenotazioni SET CodiceFiscaleCliente = @CodiceFiscaleCliente, NumeroCamera = @NumeroCamera, DataPrenotazione = @DataPrenotazione, Dal = @Dal, Al = @Al, CaparraConfirmatoria = @CaparraConfirmatoria, TariffaApplicata = @TariffaApplicata, TipoSoggiorno = @TipoSoggiorno WHERE ID = @ID", connection);

            command.Parameters.AddWithValue("@ID", prenotazione.ID);
            command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
            command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
            command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
            command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
            command.Parameters.AddWithValue("@Al", prenotazione.Al);
            command.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
            command.Parameters.AddWithValue("@TariffaApplicata", prenotazione.TariffaApplicata);
            command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno);

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
                        CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                        TariffaApplicata = (decimal)reader["TariffaApplicata"],
                        TipoSoggiorno = reader["TipoSoggiorno"].ToString()
                    });
                }
            }
        }
        return prenotazioni;
    }
}