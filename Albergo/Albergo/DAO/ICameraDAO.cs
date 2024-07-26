using Albergo.Models;

namespace Albergo.DAO
{
    public interface ICameraDAO
    {
        List<Camera> GetAllCamere();
        Camera GetCameraById(int id);
        void AddCamera(Camera camera);
        void UpdateCamera(Camera camera);
        void DeleteCamera(int id);
    }
}
