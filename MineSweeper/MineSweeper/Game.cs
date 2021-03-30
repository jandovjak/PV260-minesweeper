using System;

namespace MineSweeper
{
    public class Game : IGame
    {
        
        public IBoard CurrentBoard { get; private set; }

        public Game(int width, int height)
        {

        }

        public IBoard LeftClick(int x, int y)
        {
            if (IsValidPosition(x, y))
            {
                var tile = CurrentBoard.GetTile(x, y);
                tile.RevealTile();
            }
            
            return CurrentBoard;
        }

        public IBoard RightClick(int x, int y)
        {
            if (!IsValidPosition(x, y))
            {
                return CurrentBoard;
            }
            var tile = CurrentBoard.GetTile(x, y);
            if (IsValidFlagTile(tile))
            {
                tile.ChangeFlag();
            }
            return CurrentBoard;
        }

        private bool IsValidFlagTile(ITile tile)
        {
            return !tile.IsRevealed;
        }

        private bool IsValidPosition(int x, int y)
        {
            return (x < CurrentBoard.Width && x >= 0) && (y < CurrentBoard.Height && y >= 0);
        }
    }
}