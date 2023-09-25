using ChasseAuxTresorsApi.Classes;

namespace ChasseAuxTresorsApi.Interfaces
{
    public interface IMapService
    {
        Map CreateMapFromFile(List<string> lines);
    }
}
