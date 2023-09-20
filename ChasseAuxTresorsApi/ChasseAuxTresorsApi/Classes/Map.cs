namespace ChasseAuxTresorsApi.Classes
{
    public class Map
    {
        public int Width { get; init; }
        public int Length { get; init; }
        public Box[,] Boxes { get; set; }

        public Map(int widthNumber, int lengthNumber)
        {
            Width = widthNumber;
            Length = lengthNumber;
            Boxes = new Box[widthNumber, lengthNumber];
        }
    }
}
