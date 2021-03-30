namespace MineSweeper
{
    public interface IBoard
    {
        public int Height { get; }
        public int Width { get; }
        public ITile GetTile(int x, int y);
        public string ToString();

    }
}