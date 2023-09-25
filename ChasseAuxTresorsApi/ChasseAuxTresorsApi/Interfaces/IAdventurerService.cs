using ChasseAuxTresorsApi.Classes;

namespace ChasseAuxTresorsApi.Interfaces
{
    public interface IAdventurerService
    {
        void DoNextAction(Adventurer adventurer, Map map);
        List<Adventurer> PlaceThemOnMap(List<string> alllines, Map map);
    }
}
