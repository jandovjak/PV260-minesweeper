using System;

namespace MineSweeper
{
    public class Board : IBoard
    {
        public const int MinimalSize = 3;
        public const int MaximalSize = 50;
        private const int MinimalBombsPercentage = 20;
        private const int MaximalBombsPercentage = 60;

        public int Height { get; }
        public int Width { get; }
        private ITile[,] _tiles;

        public Board(int width, int height)
        {
            if (width < MinimalSize || height < MinimalSize || width > MaximalSize || height > MaximalSize)
            {
                throw new ArgumentOutOfRangeException();
            }

            Height = height;
            Width = width;
        }
        
        public ITile GetTile(int x, int y)
        {
            return _tiles[x, y];
        }

        public bool AllBombsFlagged()
        {
            throw new NotImplementedException();
        }

        private void setBombs()
        {
            
        }

        private void setNeighbours(int x, int y)
        {
            
        }
        
        private void initialize()
        {
            
        }
    }
}