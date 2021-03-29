using System;

namespace MineSweeper
{
    public class Board : IBoard
    {

        public Tile[][] Tiles { get; }

        public Board(int width, int height)
        {

        }
        
        public ITile GetTile(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetFlag(int x, int y)
        {
            
        }
        
        private void setBombs()
        {
            
        }

        private void setNeighbours(int x, int y)
        {
            
        }
        
        private void initialize()
        {
            
        }
    }
}