using System.Collections.Generic;

namespace MineSweeper
{
    public interface IBoard
    {
        public int Height { get; }
        public int Width { get; }
        public int BombsAmount { get; }
        public int BombsFlagged { get; }
        public int TilesFlagged { get; }
        public List<ITile> Tiles { get; }
        public ITile GetTile(int x, int y);
        public void RevealTile(int x, int y);
        public bool IsValidPosition(int x, int y);
        public void Initialize();
        public void ChangeFlag(int x, int y);
        public string ToString();
        public List<ITile> SetNeighbours(List<ITile> tiles);
        public List<ITile> SetBombs(List<ITile> tiles);
        public List<ITile> ShuffleTiles(List<ITile> tiles);
        public List<ITile> GenerateTiles();
    }
}