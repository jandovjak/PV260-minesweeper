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
        private readonly List<(int, int)> AdjacentDirections =  new List<(int dx, int dy)>
        {
            (-1, -1), (-1, -0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
        };

        public int Height { get; }
        public int Width { get; }
        public int BombsAmount { get; private set; }
        public int BombsFlagged { get; private set; }
        public int TilesFlagged { get; private set; }
        public List<ITile> Tiles { get; private set; }
        private readonly Random _randomGenerator = new Random();

        
        public Board(int width, int height)
        {
            if (width < MinimalSize || height < MinimalSize || width > MaximalSize || height > MaximalSize)
            {
                throw new ArgumentOutOfRangeException();
            }

            Height = height;
            Width = width;
            Tiles = GenerateTiles();
        }

        public Board(int width, int height, List<ITile> tiles)
        {
            if (width < MinimalSize || height < MinimalSize || width > MaximalSize || height > MaximalSize)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (width * height != tiles?.Count)
            {
                throw new ArgumentException();
            }

            Height = height;
            Width = width;
            Tiles = tiles;
            BombsAmount = Tiles.Count(tile => tile.IsBomb);
            TilesFlagged = Tiles.Count(tile => tile.IsFlag);
            TilesFlagged = Tiles.Count(tile => tile.IsFlag && tile.IsBomb);
            Tiles = SetNeighbours(Tiles);
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

            for (int i = 0; i < Width * Height; i++)
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
                    if (!tile.IsBomb)
                    {
                        continue;
                    }
                    
                    foreach (var (dx, dy) in AdjacentDirections)
                    {
                        if (IsValidPosition(x + dx, y + dy))
                        {
                            int adjIndex = CoordinatesToListIndex(x + dx, y + dy);
                            tiles[adjIndex].BombsAround += 1;
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
        
        public override string ToString()
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
            foreach (var (dx, dy) in AdjacentDirections)
            {
                if (IsValidPosition(x + dx, y + dy) && !GetTile(x + dx, y + dy).IsRevealed)
                {
                    RevealTile(x + dx, y + dy);
                }
            }
        }
        
        private int CoordinatesToListIndex(int x, int y)
        {
            return (x * Width) + y;
        }
    }
}