namespace MineSweeper
{
    public interface IBoard
    {
        public int Height { get; }
        public int Width { get; }
        public ITile GetTile(int x, int y);
        public bool IsValidPosition(int x, int y);
        public void Initialize();
        public string ToString();

    }
}