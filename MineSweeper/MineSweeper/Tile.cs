﻿using System;

namespace MineSweeper
{
    public class Tile : ITile
    {
        public bool IsBomb { get; set; }
        public int BombsAround { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlag { get; set; }

        public string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
