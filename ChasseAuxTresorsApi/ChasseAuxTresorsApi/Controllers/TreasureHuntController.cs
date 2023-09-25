using ChasseAuxTresorsApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChasseAuxTresorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreasureHuntController : ControllerBase
    {
        private readonly IMapService _mapService;
        private readonly IAdventurerService _adventurerService;
        private readonly IFileService _fileService;

        public TreasureHuntController(IMapService mapService, IAdventurerService adventurerService, IFileService fileService)
        {
            _mapService = mapService;
            _adventurerService = adventurerService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("")]
        public async Task<FileResult> GetTreasureHuntResultFromFile(IFormFile file)
        {
            var alllines = await _fileService.ConvertFileToListStringAsync(file);
            var map = _mapService.CreateMapFromFile(alllines);
            var adventurers = _adventurerService.PlaceThemOnMap(alllines, map);

            return File(new byte[] {}, "application/txt","result.txt");
        }
    }
}
