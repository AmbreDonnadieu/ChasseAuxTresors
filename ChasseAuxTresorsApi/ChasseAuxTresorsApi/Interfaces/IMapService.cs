using ChasseAuxTresorsApi.Classes;

namespace ChasseAuxTresorsApi.Interfaces
{
    public interface IMapService
    {
        Task<Map> CreateMapFromFile(IFormFile file);
    }
}
