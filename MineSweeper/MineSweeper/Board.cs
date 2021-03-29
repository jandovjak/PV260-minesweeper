using System;

namespace MineSweeper
{
    public class Board : IBoard
    {
        private const int MinimalSize = 3;
        private const int MaximalSize = 50;
        private const int MinimalBombsPercentage = 20;
        private const int MaximalBombsPercentage = 60;
        
        public Tile[][] Tiles { get; }

        public Board(int width, int height)
        {
            if (width < MinimalSize || height < MinimalSize || width > MaximalSize || height > MaximalSize)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        
        public ITile GetTile(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetFlag(int x, int y)
        {
            
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