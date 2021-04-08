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
        public List<ITile> Tiles { get; private set; }


    public Board(int width, int height)
        {
            if (width < MinimalSize || height < MinimalSize || width > MaximalSize || height > MaximalSize)
            {
                throw new ArgumentOutOfRangeException();
            }

            Height = height;
            Width = width;
            BoardSize = Height * Width;

            Tiles = GenerateTiles();
        }
        
        public ITile GetTile(int x, int y)
        {
            // TODO - nechceme tu dekrementovat najprv x a y?
            return Tiles[(x * Width) + y];
        }

        public void RevealTile(int x, int y)
        {
            if (!IsValidPosition(x, y))
                return;

            var tile = GetTile(x, y);
            tile.IsRevealed = true;

            if (tile.BombsAround == 0 && !tile.IsBomb)
                RevealAdjacentTiles(x, y);
        }

        private void RevealAdjacentTiles(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (IsValidPosition(i, j) && !GetTile(i, j).IsRevealed)
                        RevealTile(i, j);
                }
            }
        }

        public List<ITile> GenerateTiles()
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
            tiles = tiles.OrderBy((item) => _randomGenerator.Next()).ToList();
            return tiles;
        }

        public List<ITile> SetBombs(List<ITile> tiles)
        {
            var boardSize = Width * Height;
            var randomPercentage = _randomGenerator.Next(MinimalBombsPercentage, MaximalBombsPercentage);
            BombsAmount = (boardSize * randomPercentage) / 100;
            for (int i = 0; i < BombsAmount; i++)
            {
                tiles[i].IsBomb = true;
            }
            return tiles;
        }

        public List<ITile> SetNeighbours(List<ITile> tiles)
        {
            return tiles;
        }
        
        public bool IsValidPosition(int x, int y)
        {
            return (x < Width && x >= 0) && (y < Height && y >= 0);
        }

        public void Initialize()
        {
            Tiles = SetBombs(Tiles);
            Tiles = ShuffleTiles(Tiles);
            Tiles = SetNeighbours(Tiles);
        }
    }
}