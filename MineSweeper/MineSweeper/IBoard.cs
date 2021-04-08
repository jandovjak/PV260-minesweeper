using System.Collections.Generic;

namespace MineSweeper
{
    public interface IBoard
    {
        public int Height { get; }
        public int Width { get; }
        public ITile GetTile(int x, int y);
        public void RevealTile(int x, int y);
        public bool IsValidPosition(int x, int y);
        public void Initialize();
        public string ToString();
        public List<ITile> SetNeighbours(List<ITile> tiles);
        public List<ITile> SetBombs(List<ITile> tiles);
        public List<ITile> ShuffleTiles(List<ITile> tiles);
        public List<ITile> GenerateTiles();
    }
}