namespace MineSweeper
{
    public interface IGame
    {
        public IBoard LeftClick(int x, int y);
        public IBoard RightClick(int x, int y);
    }
}