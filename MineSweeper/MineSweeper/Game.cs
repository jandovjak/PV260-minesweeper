using System;

namespace MineSweeper
{
    public class Game : IGame
    {
        
        public IBoard Board { get; private set; }
        private GameStatus _gameStatus;

        public Game(int width, int height)
        {
            Board = new Board(width, height);
            Board.Initialize();
            _gameStatus = GameStatus.Playing;
        }

        public IBoard LeftClick(int x, int y)
        {
            Board.RevealTile(x, y);
            var tile = Board.GetTile(x, y);
            if (tile.IsBomb)
            {
                _gameStatus = GameStatus.Lose;
            }
            return Board;
        }

        public IBoard RightClick(int x, int y)
        {
            if (!Board.IsValidPosition(x, y))
            {
                return Board;
            }

            var tile = Board.GetTile(x, y);
            if (IsValidFlagTile(tile))
            {
                tile.ChangeFlag();
            }
            

            return Board;
        }
        public bool AreAllBombsFlagged()
        {
            throw new NotImplementedException();
        }

        private bool IsValidFlagTile(ITile tile)
        {
            return !tile.IsRevealed;
        }


    }

}