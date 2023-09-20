using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Interfaces;

namespace ChasseAuxTresorsApi.Services
{
    public class MapService : IMapService
    {
        public MapService() { }

        public async Task<Map> CreateMapFromFile(IFormFile file)
        {
            var lines = await ConvertFileToListString(file);
            var map = BuildEmptyMap(lines);
            PopulateMountains(map, lines);
            PopulateTreasure(map, lines);
            return map;
        }


        public async Task<List<string>> ConvertFileToListString(IFormFile file)
        {
            var allLines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    allLines.Append(await reader.ReadLineAsync());
                }
            }
            return allLines;
        }

        public Map BuildEmptyMap(List<string> lines)
        {
            //we begin to get Map Info
            var buildingMapLine = lines.Where(l => l.StartsWith('C'));

            if (!buildingMapLine.Any())
                throw new Exception("We couldn't find the line that gives building information to create the map. Please, select an other file.");
            if (buildingMapLine.Count() > 1)
                throw new Exception("There is more than one map in the file. Please, keep only one map for the process.");

            var splitLine = buildingMapLine.First().Split(" - ").ToArray();
            if (splitLine.Length != 3)
                throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

            var map = new Map(int.Parse(splitLine[1]), int.Parse(splitLine[2]));

            return map;
        }

        public void PopulateMountains(Map map, List<string> lines)
        {
            foreach (var line in lines.Where(l => l.StartsWith("M")))
            {
                var splitLine = line.Split(" - ").ToArray();
                if (splitLine.Length != 3)
                    throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

                if (int.TryParse(splitLine[1], out int vertical) && int.TryParse(splitLine[2], out int horizontal))
                    map.Boxes[vertical, horizontal] = new Box { type = BoxType.Moutain };
                else
                    Console.WriteLine($"WARNING : Counldn't place a moutain because something went wrong with line {line}.");
            }
        }

        public void PopulateTreasure(Map map, List<string> lines)
        {
            foreach (var line in lines.Where(l => l.StartsWith("T")))
            {
                var splitLine = line.Split(" - ").ToArray();
                if (splitLine.Length != 4)
                    throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

                if (int.TryParse(splitLine[1], out int vertical) && int.TryParse(splitLine[2], out int horizontal) && int.TryParse(splitLine[3], out int treasureCount))
                    map.Boxes[vertical, horizontal] = new Box { type = BoxType.Treasure, TreasureCount = treasureCount };
                else
                    Console.WriteLine($"WARNING : Counldn't place a treasure because something went wrong with line {line}.");
            }
        }
    }
}
