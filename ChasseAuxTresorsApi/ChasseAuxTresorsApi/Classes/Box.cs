namespace ChasseAuxTresorsApi.Classes
{
    public class Box
    {
        public BoxType Type { get; set; } = BoxType.Plain;
        public int TreasureCount { get; set; } = 0;
        public Adventurer? Adventurer { get; set; } = null;
    }

    public enum BoxType
    {
        Plain, 
        Moutain, 
        Treasure        
    }
}
