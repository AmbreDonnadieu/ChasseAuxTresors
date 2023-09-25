namespace ChasseAuxTresorsApi.Classes
{
    public class Map
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public Box[,] Boxes { get; set; }

        public Map(int widthNumber, int heightNumber)
        {
            Width = widthNumber;
            Height = heightNumber;
            Boxes = new Box[widthNumber, heightNumber];

            for(int i = 0; i < widthNumber; i++)
            {
                for (int j = 0; j < heightNumber; j++)
                {
                    Boxes[i, j] = new Box();
                }
            }
        }
    }
}
