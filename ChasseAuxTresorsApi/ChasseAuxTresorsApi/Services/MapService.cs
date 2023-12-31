﻿using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Interfaces;
using System.ComponentModel;

namespace ChasseAuxTresorsApi.Services
{
    public class MapService : IMapService
    {
        public MapService() { }

        public Map CreateMapFromListOfLines(List<string> lines)
        {
            var map = BuildEmptyMap(lines);
            foreach (var line in lines)
            {
                if (line.StartsWith("M"))
                {
                    PopulateMountain(map, line);
                    continue;
                }
                if (line.StartsWith("T"))
                {
                    PopulateTreasure(map, line);
                    continue;
                }
            }
            return map;
        }

        public Map BuildEmptyMap(List<string> lines)
        {
            //we begin to get Map Info
            var buildingMapLine = lines.Where(l => l.StartsWith('C'));

            if (!buildingMapLine.Any())
                throw new Exception("We couldn't find the line that gives building information to create the map. Please, select an other file.");
            if (buildingMapLine.Count() > 1)
                throw new Exception("There is more than one map in the file. Please, keep only one map for the process.");

            var splitLine = buildingMapLine.First().Trim().Split("-").ToArray();
            if (splitLine.Length != 3)
                throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

            if (int.TryParse(splitLine[1], out int horizontal) && int.TryParse(splitLine[2], out int vertical))
                return new Map(horizontal, vertical);
            else
                throw new Exception("We couldn't create the map because we could read horizontal or vertical number.");
        }

        public void PopulateMountain(Map map, string line)
        {
            var splitLine = line.Trim().Split("-").ToArray();
            if (splitLine.Length != 3)
                throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

            if (int.TryParse(splitLine[1], out int horizontal) && int.TryParse(splitLine[2], out int vertical))
                map.Boxes[horizontal, vertical].Type = BoxType.Moutain;
            else
                throw new WarningException($"WARNING : Counldn't place a moutain because something went wrong with line {line}.");
        }

        public void PopulateTreasure(Map map, string line)
        {
            var splitLine = line.Trim().Split("-").ToArray();
            if (splitLine.Length != 4)
                throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

            if (int.TryParse(splitLine[1], out int horizontal) && int.TryParse(splitLine[2], out int vertical) &&
                int.TryParse(splitLine[3], out int treasureCount))
            {
                map.Boxes[horizontal, vertical].Type = BoxType.Treasure;
                map.Boxes[horizontal, vertical].TreasureCount = treasureCount;
            }
            else
                throw new WarningException($"WARNING : Counldn't place a treasure because something went wrong with line {line}.");
        }
    
        public string WriteMap(Map map)
        {
            string result="";
            for(var i = 0; i<map.Width; i++)
            {
                for(var j = 0; j < map.Height; j++)
                {
                    if (map.Boxes[i, j].Type == BoxType.Moutain)
                        result = result + "M ";
                    else if (map.Boxes[i, j].Adventurer != null)
                        result = result + $"A({map.Boxes[i, j].Adventurer?.Name} - {map.Boxes[i, j].Adventurer?.TreasureCount}) ";
                    else if (map.Boxes[i, j].Type == BoxType.Treasure)
                        result = result + $"T({map.Boxes[i, j].TreasureCount}) ";
                    else
                        result = result + ". ";
                    result = result + " | ";
                }
                result = result + "\n";
            }

            return result;
        }
    }
}
