namespace MineSweeper
{
    public interface ITile
    {
        public bool IsBomb { get; set; }
        public int BombsAround { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlag { get; set; }
        public void RevealTile();
        public void ChangeFlag();
        public string ToString();
    }
}