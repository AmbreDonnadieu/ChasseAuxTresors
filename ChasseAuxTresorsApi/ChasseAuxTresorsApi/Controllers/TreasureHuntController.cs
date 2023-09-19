using ChasseAuxTresorsApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChasseAuxTresorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreasureHuntController : ControllerBase
    {
        private ICarteService _carteService;

        public TreasureHuntController(ICarteService carteService)
        {
            _carteService = carteService;
        }

        [HttpGet]
        [Route("")]
        public FileResult GetTreasureHuntResultFromFile(IFormFile file)
        {
            var carte = _carteService.CreateCarte(file);
            return File(new byte[] {}, "application/txt","result.txt");
        }
    }
}
