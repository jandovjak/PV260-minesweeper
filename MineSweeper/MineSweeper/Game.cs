namespace MineSweeper
{
    public class Game : IGame
    {
        

        private IBoard _realBoard;
        public IBoard CurrentBoard { get; }

        public Game(int width, int height)
        {

        }

        public void Move(int x, int y)
        {
            
        }
    }
}