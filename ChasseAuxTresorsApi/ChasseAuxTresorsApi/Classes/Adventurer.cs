﻿namespace ChasseAuxTresorsApi.Classes
{
    public class Adventurer
    {
        public string Name { get; init; }
        public string Mouvements { get; init; }
        public int LastMouvementxIndex { get; set; } = 0;
        public Position Position { get; set; } = new Position(0,0);
        public Orientation Orientation { get; set; } = Orientation.North;

        public int TreasureCount { get; set; } = 0;

        public Adventurer(string name, Position position, Orientation orientation, string mouvements)
        {
            Name = name;
            Position = position;
            Orientation = orientation;
            Mouvements = mouvements;
            LastMouvementxIndex = 0;
            TreasureCount = 0;
        }
    }

    public enum Orientation
    {
        North, 
        South, 
        West,
        East
    }

    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
