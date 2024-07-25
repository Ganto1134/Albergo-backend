using Albergo.Models;
using System.Data.SqlClient;


namespace Albergo.DAO {
    public class UserDAO : IUserDAO
    {
        private readonly string _connectionString;

        public UserDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            User user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            ID = (int)reader["ID"],
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString(),
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Citta = reader["Citta"].ToString(),
                            Provincia = reader["Provincia"].ToString(),
                            Email = reader["Email"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Cellulare = reader["Cellulare"].ToString()
                        };
                    }
                }
            }
            return user;
        }

        public async Task CreateUserAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO Users (Username, PasswordHash, Role, CreatedAt, CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) " +
                    "VALUES (@Username, @PasswordHash, @Role, @CreatedAt, @CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)", connection);

                command.Parameters.AddWithValue("@Username", user.Username ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Role", user.Role ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt == DateTime.MinValue ? (object)DBNull.Value : user.CreatedAt);
                command.Parameters.AddWithValue("@CodiceFiscale", user.CodiceFiscale ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Cognome", user.Cognome ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Nome", user.Nome ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Citta", user.Citta ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Provincia", user.Provincia ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Telefono", user.Telefono ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Cellulare", user.Cellulare ?? (object)DBNull.Value);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, Role = @Role, CodiceFiscale = @CodiceFiscale, " +
                    "Cognome = @Cognome, Nome = @Nome, Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare " +
                    "WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", user.ID);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@CodiceFiscale", user.CodiceFiscale);
                command.Parameters.AddWithValue("@Cognome", user.Cognome);
                command.Parameters.AddWithValue("@Nome", user.Nome);
                command.Parameters.AddWithValue("@Citta", user.Citta);
                command.Parameters.AddWithValue("@Provincia", user.Provincia);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Telefono", user.Telefono);
                command.Parameters.AddWithValue("@Cellulare", user.Cellulare);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Users", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            ID = (int)reader["ID"],
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString(),
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Citta = reader["Citta"].ToString(),
                            Provincia = reader["Provincia"].ToString(),
                            Email = reader["Email"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Cellulare = reader["Cellulare"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}