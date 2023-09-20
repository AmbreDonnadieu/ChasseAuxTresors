namespace ChasseAuxTresorsApi.Classes
{
    public class Box
    {
        public BoxType type { get; set; } = BoxType.Plain;
        public int TreasureCount { get; set; } = 0;
        public Adventurer? adventurer { get; set; } = null;
    }

    public enum BoxType
    {
        Moutain, 
        Treasure, 
        Plain
    }
}
