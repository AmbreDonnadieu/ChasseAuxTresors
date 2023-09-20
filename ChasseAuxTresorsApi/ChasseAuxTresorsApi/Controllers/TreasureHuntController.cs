using ChasseAuxTresorsApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChasseAuxTresorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreasureHuntController : ControllerBase
    {
        private IMapService _mapService;

        public TreasureHuntController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet]
        [Route("")]
        public FileResult GetTreasureHuntResultFromFile(IFormFile file)
        {
            var carte = _mapService.CreateMapFromFile(file);
            return File(new byte[] {}, "application/txt","result.txt");
        }
    }
}
