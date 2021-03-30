using System;
using System.Collections.Generic;
using System.Linq;

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
        public int _bombsAmount { get; private set; }
        
        private Random _randomGenerator = new Random();
        private List<ITile> _tiles;
        private int _boardSize;


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
            return _tiles[(x * Width) + y];
        }

        private void generateTiles()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                _tiles[i] = new Tile();
                if (i < _bombsAmount)
                {
                    _tiles[i].IsBomb = true;
                }
            }
        }

        private void shuffleTiles()
        {
            _tiles.OrderBy((item) => _randomGenerator.Next());
        }
        
        private void setBombs()
        {
            var boardSize = Width * Height;
            var randomPercentage = _randomGenerator.Next(MinimalBombsPercentage, MaximalBombsPercentage);
            _bombsAmount = (boardSize * randomPercentage) / 100;
            
        }

        private void setNeighbours()
        {
            
        }
        
        private void initialize()
        {
            setBombs();
            setNeighbours();
        }
    }
}