using System;

namespace MineSweeper
{
    public class Game : IGame
    {
        
        public IBoard CurrentBoard { get; private set; }

        public Game(int width, int height)
        {

        }

        public IBoard Move(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}