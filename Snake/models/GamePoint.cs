namespace Snake.models
{/// <summary>
/// A játéktáblán egy pontot jelképező egység (étel vagy kígyó)
/// </summary>
    public class GamePoint
    {
        public GamePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}