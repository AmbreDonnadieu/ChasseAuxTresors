﻿using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Interfaces;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace ChasseAuxTresorsApi.Services
{
    public class AdventurerService : IAdventurerService
    {
        public AdventurerService() { }

        public void DoNextAction(Adventurer adventurer, Map map)
        {
            if (adventurer.LastMouvementxIndex < adventurer.Mouvements.Length)
            {
                var nextAction = adventurer.Mouvements[adventurer.LastMouvementxIndex];
                switch (nextAction)
                {
                    case 'A':
                        MoveForward(adventurer, map);
                        break;

                    case 'D':
                        TurnRight(adventurer);
                        break;

                    case 'G':
                        TurnLeft(adventurer);
                        break;

                    default:
                        Console.WriteLine($"The action was not recognize at index {adventurer.LastMouvementxIndex}. We juste pass and go to next index.");
                        break;
                }
                adventurer.LastMouvementxIndex++;
            }
            else { Console.WriteLine($"{adventurer.Name} has already done all its mouvements."); }
        }

        public void TurnRight(Adventurer adventurer)
        {
            switch (adventurer.Orientation)
            {
                case Orientation.North:
                    adventurer.Orientation = Orientation.East;
                    break;

                case Orientation.South:
                    adventurer.Orientation = Orientation.West;
                    break;

                case Orientation.East:
                    adventurer.Orientation = Orientation.South;
                    break;

                case Orientation.West:
                    adventurer.Orientation = Orientation.North;
                    break;

                default:
                    throw new WarningException($"TurnRight : This shouldn't happen");
            }
        }
        public void TurnLeft(Adventurer adventurer)
        {
            switch (adventurer.Orientation)
            {
                case Orientation.North:
                    adventurer.Orientation = Orientation.West;
                    break;

                case Orientation.South:
                    adventurer.Orientation = Orientation.East;
                    break;

                case Orientation.East:
                    adventurer.Orientation = Orientation.North;
                    break;

                case Orientation.West:
                    adventurer.Orientation = Orientation.South;
                    break;

                default:
                    throw new WarningException($"TurnRight : This shouldn't happen");

            }
        }

        public void MoveForward(Adventurer adventurer, Map map)
        {
            var temporaryPosition = new Position(adventurer.Position.x, adventurer.Position.y);
            switch (adventurer.Orientation)
            {
                case Orientation.North:
                    if (CanMoveFoward(new Position(temporaryPosition.x, temporaryPosition.y-1), map))
                        temporaryPosition.y--;
                    break;

                case Orientation.South:
                    if (CanMoveFoward(new Position(temporaryPosition.x, temporaryPosition.y+1), map))
                        temporaryPosition.y++;
                    break;

                case Orientation.East:
                    if (CanMoveFoward(new Position(temporaryPosition.x+1, temporaryPosition.y), map))
                        temporaryPosition.x++;
                    break;

                case Orientation.West:
                    if (CanMoveFoward(new Position(temporaryPosition.x-1, temporaryPosition.y), map))
                        temporaryPosition.x--;
                    break;

                default:
                    break;
            }

            map.Boxes[adventurer.Position.x, adventurer.Position.y].Adventurer = null;
            adventurer.Position = temporaryPosition;
            map.Boxes[temporaryPosition.x, temporaryPosition.y].Adventurer = adventurer;

            if (map.Boxes[temporaryPosition.x, temporaryPosition.y].Type == BoxType.Treasure &&
                map.Boxes[temporaryPosition.x, temporaryPosition.y].TreasureCount > 0)
            {
                map.Boxes[temporaryPosition.x, temporaryPosition.y].TreasureCount--;
                adventurer.TreasureCount++;
            }

        }

        public bool CanMoveFoward(Position newPositionAdventurer, Map map)
        {
            if (newPositionAdventurer.x < 0 || newPositionAdventurer.x >= map.Width ||
                newPositionAdventurer.y < 0 || newPositionAdventurer.y >= map.Height)
            {
                return false;
            }

            if (map.Boxes[newPositionAdventurer.x, newPositionAdventurer.y].Type == BoxType.Moutain || 
                map.Boxes[newPositionAdventurer.x, newPositionAdventurer.y].Adventurer != null)
            {
                return false;
            }
            return true;
        }


        public List<Adventurer> PlaceThemOnMap(List<string> alllines, Map map)
        {
            List<Adventurer> adventurers = new List<Adventurer>();
            foreach (string line in alllines.Where(l => l.StartsWith('A')))
            {
                var splitLine = string.Concat(line.Where(c => !char.IsWhiteSpace(c))).Split("-").ToArray();
                if (splitLine.Length != 6)
                    throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

                if (int.TryParse(splitLine[2], out int abscissa) && int.TryParse(splitLine[3], out int ordinate) &&
                    TryParseStringToOrientation(splitLine[4], out Orientation direction))
                {
                    var newAdventurer = new Adventurer(splitLine[1], new Position(abscissa, ordinate), direction, splitLine[5]);
                    adventurers.Add(newAdventurer);
                    map.Boxes[abscissa,ordinate].Adventurer = newAdventurer;
                }
                else
                    Console.WriteLine($"WARNING : Counldn't place a moutain because something went wrong with line {line}.");
            }
            return adventurers;
        }

        private bool TryParseStringToOrientation(string s, out Orientation orientation)
        {
            var res = false;
            switch (s.Trim())
            {
                case "S":
                case "s":
                    orientation = Orientation.South;
                    res = true;
                    break;

                case "N":
                case "n":
                    orientation = Orientation.North;
                    res = true;
                    break;

                case "E":
                case "e":
                    orientation = Orientation.East;
                    res = true;
                    break;

                case "O":
                case "o":
                    orientation = Orientation.West;
                    res = true;
                    break;

                default:
                    orientation = Orientation.North;
                    break;
            }
            return res;
        }
    }
}
