using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public int BombsFlagged { get; private set; }
        public int TilesFlagged { get; private set; }

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
            int index = CoordinatesToListIndex(x, y);
            return Tiles[index];
        }

        public void RevealTile(int x, int y)
        {
            if (!IsValidPosition(x, y) || GetTile(x, y).IsFlag)
                return;
            
            var tile = GetTile(x, y);
            tile.IsRevealed = true;

            if (tile.BombsAround == 0 && !tile.IsBomb)
                RevealAdjacentTiles(x, y);
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
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int index = CoordinatesToListIndex(x, y);
                    ITile tile = tiles[index];

                    if (tile.IsBomb)
                    {
                        for (int i = x - 1; i <= x + 1; i++)
                        {
                            for (int j = y - 1; j <= y + 1; j++)
                            {
                                if ((i != x || j != y) && IsValidPosition(i, j))
                                {
                                    int adjIndex = CoordinatesToListIndex(i, j);
                                    tiles[adjIndex].BombsAround += 1;
                                }
                            }
                        }
                    }
                }
            }

            return tiles;
        }
        
        public bool IsValidPosition(int x, int y)
        {
            return (x < Width && x >= 0) && (y < Height && y >= 0);
        }

        public void ChangeFlag(int x, int y)
        {
            var tile = GetTile(x, y);
            tile.ChangeFlag();
            if (tile.IsBomb)
            {
                if (tile.IsFlag)
                {
                    BombsFlagged++;
                }
                else
                {
                    BombsFlagged--;
                }
            }
            
            if (tile.IsFlag)
            {
                TilesFlagged++;
            }
            else
            {
                TilesFlagged--;
            }
        }

        public void Initialize()
        {
            Tiles = SetBombs(Tiles);
            Tiles = ShuffleTiles(Tiles);
            Tiles = SetNeighbours(Tiles);
        }

        public string ToString()
        {
            var builder = new StringBuilder();

            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    builder.Append(GetTile(x, y).ToString());
                }
                builder.Append("\n");
            }

            return builder.ToString();
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
        
        private int CoordinatesToListIndex(int x, int y)
        {
            return (x * Width) + y;
        }
    }
}