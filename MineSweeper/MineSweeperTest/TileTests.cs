using MineSweeper;
using NUnit.Framework;

namespace MineSweeperTest
{
    public class TileTests
    {
        [Test]
        public void ChangeFlagFromFlagged()
        {
            var tile = new Tile {IsFlag = true};
            tile.ChangeFlag();
            Assert.IsFalse(tile.IsFlag);
        }
        
        [Test]
        public void ChangeFlagFromUnflagged()
        {
            var tile = new Tile {IsFlag = false};
            tile.ChangeFlag();
            Assert.IsTrue(tile.IsFlag);
        }
        
        [Test]
        public void RevealRevealed()
        {
            var tile = new Tile {IsRevealed = true};
            tile.RevealTile();
            Assert.IsTrue(tile.IsRevealed);
        }
        
        [Test]
        public void RevealNotRevealed()
        {
            var tile = new Tile {IsRevealed = false};
            tile.RevealTile();
            Assert.IsTrue(tile.IsRevealed);
        }
        
        [Test]
        public void NotRevealedBomb_ToString()
        {
            var tile = new Tile {IsRevealed = false, IsBomb = true};
            Assert.Equals(tile.ToString(), ".");
        }
        
        [Test]
        public void NotRevealedEmptyTile_ToString()
        {
            var tile = new Tile {IsRevealed = false};
            Assert.Equals(tile.ToString(), ".");
        }
        
        [Test] 
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void RevealedEmptyTile_ToString(int bombsAround)
        {
            var tile = new Tile {IsRevealed = true, BombsAround = bombsAround};
            Assert.Equals(tile.ToString(), bombsAround.ToString());
        }
    }
}