using ChasseAuxTresorsApi.Classes;

namespace ChasseAuxTresorsApi.Interfaces
{
    public interface IMapService
    {
        Map CreateMapFromListOfLines(List<string> lines);
    }
}
