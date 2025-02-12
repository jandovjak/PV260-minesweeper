using System;

namespace MineSweeper
{
    public class Game : IGame
    {
        
        public IBoard Board { get; private set; }
        public GameStatus GameStatus { get; private set; }

        public Game(int width, int height)
        {
            Board = new Board(width, height);
            Board.Initialize();
            GameStatus = GameStatus.Playing;
        }
        
        public Game(IBoard board)
        {
            Board = board;
            GameStatus = GameStatus.Playing;
        }

        public IBoard LeftClick(int x, int y)
        {
            x--;
            y--;
            var tile = Board.GetTile(x, y);
            if (tile.IsFlag)
            {
                return Board;
            }
            Board.RevealTile(x, y);
            if (tile.IsBomb)
            {
                GameStatus = GameStatus.Lose;
            }
            return Board;
        }

        public IBoard RightClick(int x, int y)
        {
            x--;
            y--;
            if (!Board.IsValidPosition(x, y))
            {
                return Board;
            }

            var tile = Board.GetTile(x, y);
            if (!tile.IsRevealed)
            {
                Board.ChangeFlag(x, y);
            }

            if (AreAllBombsFlagged())
            {
                GameStatus = GameStatus.Win;
            }

            return Board;
        }
        public bool AreAllBombsFlagged()
        {
            return Board.BombsAmount == Board.BombsFlagged && Board.BombsFlagged == Board.TilesFlagged;
        }


    }

}