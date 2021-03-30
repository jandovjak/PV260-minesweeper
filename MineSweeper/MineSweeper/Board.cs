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
        public int BombsAmount { get; private set; }

        private readonly Random _randomGenerator = new Random();
        public readonly int BoardSize;
        private List<ITile> _tiles;


        public Board(int width, int height)
        {
            if (width < MinimalSize || height < MinimalSize || width > MaximalSize || height > MaximalSize)
            {
                throw new ArgumentOutOfRangeException();
            }

            Height = height;
            Width = width;
            BoardSize = Height * Width;

            _tiles = generateTiles();
            _tiles = setBombs(_tiles);
            _tiles = shuffleTiles(_tiles);
            _tiles = setNeighbours(_tiles);
        }
        
        public ITile GetTile(int x, int y)
        {
            return _tiles[(x * Width) + y];
        }

        public List<ITile> generateTiles()
        {
            var tiles = new List<ITile>();

            for (int i = 0; i < BoardSize; i++)
            {
                tiles.Add(new Tile());
            }

            return tiles;
        }

        public List<ITile> ShuffleTiles(List<ITile> tiles)
        {
            // TODO
            tiles = tiles.OrderBy((item) => _randomGenerator.Next());
            return tiles;
        }

        public List<ITile> SetBombs(List<ITile> tiles)
        {
            var boardSize = Width * Height;
            var randomPercentage = _randomGenerator.Next(MinimalBombsPercentage, MaximalBombsPercentage);
            BombsAmount = (boardSize * randomPercentage) / 100;

            // TODO
            if (i < BombsAmount)
            {
                _tiles[i].IsBomb = true;
            }

            return tiles;
        }

        public List<ITile> SetNeighbours(List<ITile> tiles)
        {
            throw new NotImplementedException();
        }
    }
}