using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public List<Camera> GetAllCamere()
        {
            var camere = new List<Camera>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Camere", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

        public Camera GetCameraById(int id)
        {
            Camera camera = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Camere WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", id);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
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

        public void AddCamera(Camera camera)
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

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateCamera(Camera camera)
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

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCamera(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Camere WHERE Numero = @Numero", connection);
                command.Parameters.AddWithValue("@Numero", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
