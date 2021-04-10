using System;

namespace MineSweeper
{
    public class Tile : ITile
    {
        public bool IsBomb { get; set; }
        public int BombsAround { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlag { get; set; }


        public void RevealTile()
        {
            IsRevealed = true;
        }
        
        public void ChangeFlag()
        {
            IsFlag = !IsFlag;
        }
        public override string ToString()
        {
            if (!IsRevealed)
                return ".";
            if (IsBomb)
                return "x";
            return BombsAround.ToString();
        }
    }
}
