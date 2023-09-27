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
            // we convert file to list of strings to facilitate treatement after
            var alllines = await _fileService.ConvertFileToListStringAsync(file);

            //from this list, we create the map
            var map = _mapService.CreateMapFromListOfLines(alllines);
            //then we create and place the adventurers
            var adventurers = _adventurerService.PlaceThemOnMap(alllines, map);

            // we will write in a string every state of the map and first, we write initial state
            var allMapTurns = $"Initialization : \n{_mapService.WriteMap(map)}\n\n";

            //if there is no adventurers, there will be no evolution so we return the result directly
            if (!adventurers.Any())
            {
                allMapTurns = $"{allMapTurns} There were no adventurers on the map. It stays at initialization state.";
                return File(Encoding.UTF8.GetBytes(allMapTurns), "application/txt", "result.txt");
            }

            // we get how many turn there will be by checking in diretly the mouvement of all adventurer
            var numberOfTurn = adventurers.Select(a => a.Mouvements.Length).Max();
            for (var i = 0; i <= numberOfTurn; i++)
            {
                // at each turn, we do the action of each adventurer
                foreach (var adv in adventurers)
                {
                    _adventurerService.DoNextAction(adv, map);
                }
                allMapTurns = $"{allMapTurns} TURN {i} :\n{_mapService.WriteMap(map)}\n\n";
            }

            // when all turns were done, we throw the result
            return File(Encoding.UTF8.GetBytes(allMapTurns), "application/txt", "result.txt");
        }
    }
}
