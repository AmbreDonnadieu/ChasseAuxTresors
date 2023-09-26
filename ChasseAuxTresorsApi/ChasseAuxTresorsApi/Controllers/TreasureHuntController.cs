using ChasseAuxTresorsApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        [HttpPost]
        [Route("")]
        public async Task<FileResult> GetTreasureHuntResultFromFile(IFormFile file)
        {
            var alllines = await _fileService.ConvertFileToListStringAsync(file);
            var map = _mapService.CreateMapFromListOfLines(alllines);
            var adventurers = _adventurerService.PlaceThemOnMap(alllines, map);

            var allMapTurns = $"Initialization : \n{_mapService.WriteMap(map)}\n\n";
            if (!adventurers.Any())
            {
                allMapTurns = $"{allMapTurns} There were no adventurers on the map. It stays at initialization state.";
                return File(Encoding.UTF8.GetBytes(allMapTurns), "application/txt", "result.txt");
            }

            var numberOfTurn = adventurers.Select(a => a.Mouvements.Length).Max();
            for (var i = 0; i <= numberOfTurn; i++)
            {
                foreach (var adv in adventurers)
                {
                    _adventurerService.DoNextAction(adv, map);
                }
                allMapTurns = $"{allMapTurns} TURN {i} :\n{_mapService.WriteMap(map)}\n\n";
            }

            return File(Encoding.UTF8.GetBytes(allMapTurns), "application/txt", "result.txt");
        }
    }
}
