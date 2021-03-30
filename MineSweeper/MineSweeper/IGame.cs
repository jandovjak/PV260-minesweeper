namespace MineSweeper
{
    public interface IGame
    {
        public IBoard Move(int x, int y);
    }
}