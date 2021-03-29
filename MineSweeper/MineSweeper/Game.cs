namespace MineSweeper
{
    public class Game : IGame
    {
        private const int MinimalSize = 3;
        private const int MaximalSize = 50;
        private const int MinimalBombsPercentage = 20;
        private const int MaximalBombsPercentage = 60;

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