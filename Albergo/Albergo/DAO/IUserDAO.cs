using Albergo.Models;

namespace Albergo.DAO {

    public interface IUserDAO
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
    }
}