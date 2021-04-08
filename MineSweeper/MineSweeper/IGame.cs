namespace MineSweeper
{
    public interface IGame
    {
        public IBoard Board { get; }
        public GameStatus GameStatus { get; }
        public IBoard LeftClick(int x, int y);
        public IBoard RightClick(int x, int y);
        public bool AreAllBombsFlagged();
    }
}