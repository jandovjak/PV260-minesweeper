namespace MineSweeper
{
    public interface ITile
    {
        public bool IsBomb { get; set; }
        public int BombsAround { get; set; }
        public bool IsReviewed { get; set; }
        public bool IsFlag { get; set; }
        public string ToString();
    }
}