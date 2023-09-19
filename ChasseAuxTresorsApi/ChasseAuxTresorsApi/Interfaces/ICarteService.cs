using ChasseAuxTresorsApi.Classes;

namespace ChasseAuxTresorsApi.Interfaces
{
    public interface ICarteService
    {
        Carte CreateCarte(IFormFile File);
    }
}
