namespace MineSweeper
{
    public interface IBoard
    {
        public Tile[][] Tiles { get; }
        public ITile GetTile(int x, int y);
        public void SetFlag(int x, int y);
        public string ToString();
        public bool AllBombsFlagged();
    }
}