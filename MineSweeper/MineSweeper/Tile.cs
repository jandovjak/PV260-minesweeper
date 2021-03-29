using System;

namespace MineSweeper
{
    public class Tile
    {
        public bool IsBomb { get; set; }
        public int BombsAround { get; set; }
        public bool IsReviewed { get; set; }
        public bool IsFlag { get; set; }
    }
}
