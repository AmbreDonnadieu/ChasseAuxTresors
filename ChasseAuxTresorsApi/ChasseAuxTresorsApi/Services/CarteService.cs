using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Interfaces;

namespace ChasseAuxTresorsApi.Services
{
    public class CarteService : ICarteService
    {
        public CarteService() { }

        public Carte CreateCarte(IFormFile File)
        {
            return new Carte();
        }

    }
}
