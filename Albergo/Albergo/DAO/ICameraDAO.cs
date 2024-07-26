using Albergo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albergo.DAO
{
    public interface ICameraDAO
    {
        Task<List<Camera>> GetAllCamereAsync();
        Task<Camera> GetCameraByIdAsync(int id);
        Task AddCameraAsync(Camera camera);
        Task UpdateCameraAsync(Camera camera);
        Task DeleteCameraAsync(int id);
    }
}