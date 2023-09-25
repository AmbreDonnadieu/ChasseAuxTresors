using ChasseAuxTresorsApi.Classes;
using ChasseAuxTresorsApi.Interfaces;

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
                    Console.WriteLine();
                    break;
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
                    Console.WriteLine();
                    break;
            }
        }

        public void MoveForward(Adventurer adventurer, Map map)
        {
            var temporaryPosition = adventurer.Position;
            switch (adventurer.Orientation)
            {
                case Orientation.North:
                    if (CanMoveFoward(new Position(temporaryPosition.x, temporaryPosition.y--), map))
                        temporaryPosition.y--;
                    break;

                case Orientation.South:
                    if (CanMoveFoward(new Position(temporaryPosition.x, temporaryPosition.y++), map))
                        temporaryPosition.y++;
                    break;

                case Orientation.East:
                    if (CanMoveFoward(new Position(temporaryPosition.x++, temporaryPosition.y), map))
                        temporaryPosition.x++;
                    break;

                case Orientation.West:
                    if (CanMoveFoward(new Position(temporaryPosition.x--, temporaryPosition.y), map))
                        temporaryPosition.x--;
                    break;

                default:
                    Console.WriteLine();
                    break;
            }
            adventurer.Position = temporaryPosition;
        }

        public bool CanMoveFoward(Position positionAdventurer, Map map)
        {
            if (map.Boxes[positionAdventurer.x, positionAdventurer.y].Type == BoxType.Moutain &&
                positionAdventurer.x > 0 && positionAdventurer.x < map.Width &&
                positionAdventurer.y > 0 && positionAdventurer.y < map.Height)
                return false;
            return true;
        }

        public List<Adventurer> PlaceThemOnMap(List<string> alllines, Map map)
        {
            List<Adventurer> adventurers = new List<Adventurer>();
            foreach (string line in alllines.Where(l => l.StartsWith('A')))
            {
                var splitLine = line.Trim().Split("-").ToArray();
                if (splitLine.Length != 6)
                    throw new Exception("We didn't get the right number of parameters. Please, correct the file and do the process one more time.");

                if (int.TryParse(splitLine[2], out int horizontal) && int.TryParse(splitLine[3], out int vertical) &&
                    TryParseStringToOrientation(splitLine[4], out Orientation orientation))
                    adventurers.Add(new Adventurer(splitLine[1], new Position(horizontal, vertical), orientation, splitLine[5]));
                else
                            Console.WriteLine($"WARNING : Counldn't place a moutain because something went wrong with line {line}.");

            }
            return adventurers;
        }

        public bool TryParseStringToOrientation(string s, out Orientation orientation)
        {
            var res = false;
            switch (s)
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
