using System;
using MineSweeper;
using NUnit.Framework;
using System.Linq;

namespace MineSweeperTest
{
    public class BoardTests
    {
        [Test]
        [TestCase(Board.MinimalSize - 1, Board.MinimalSize - 1)]
        [TestCase(Board.MinimalSize - 1, Board.MinimalSize)]
        [TestCase(Board.MinimalSize, Board.MinimalSize - 1)]
        [TestCase(Board.MaximalSize + 1, Board.MaximalSize + 1)]
        [TestCase(Board.MaximalSize + 1, Board.MaximalSize)]
        [TestCase(Board.MaximalSize, Board.MaximalSize + 1)]
        public void InvalidSizes_ThrowsException(int width, int height)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(width, height));
        }

        [Test]
        public void CheckBombsAround()
        {
            var board = new Board(10, 10);
            for(int x = 0; x < board.Height; x++) {
                for(int y = 0; y < board.Width; y++)
                {
                    if (board.GetTile(x, y).IsBomb)
                    {
                        continue;
                    }
                    var expected = 0;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (board.IsValidPosition(i, j) && board.GetTile(i, j).IsBomb)
                            {
                                expected++;
                            }
                        }
                    }
                    Assert.AreEqual(expected, board.GetTile(x, y).BombsAround);
                }
            }
        }
        
        [Test]
        [TestCase(-1, 0, 3, 3)]
        [TestCase(0, -1, 3, 3)]
        [TestCase(-5, -5, 3, 3)]
        [TestCase(6, 5, 4, 4)]
        [TestCase(5, 5, 5, 5)]
        public void CheckIsValidPosition_False(int x, int y, int width, int height)
        {
            var board = new Board(width, height);
            Assert.IsFalse(board.IsValidPosition(x, y));
        }
        
        [Test]
        [TestCase(0, 0, 3, 3)]
        [TestCase(1, 0, 4, 4)]
        [TestCase(1, 1, 5, 5)]
        [TestCase(4, 2, 5, 5)]
        [TestCase(3, 4, 5, 5)]
        public void CheckIsValidPosition_True(int x, int y, int width, int height)
        {
            var board = new Board(width, height);
            Assert.IsTrue(board.IsValidPosition(x, y));
        }

        [Test]
        public void CheckRevealTile_GivenTileIsBomb()
        {
            var board = new Board(5, 5);
            var tile = board.GetTile(1, 1);
            var tiles = board.Tiles;
            int numRevealedTilesInitial = tiles.Count(tile => tile.IsRevealed);

            Assert.IsFalse(tile.IsRevealed);
            tile.IsBomb = true;
            board.RevealTile(1, 1);
            Assert.IsTrue(tile.IsRevealed);

            int numRevealedTilesAfter = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(numRevealedTilesInitial + 1, numRevealedTilesAfter);
        }
    }
}