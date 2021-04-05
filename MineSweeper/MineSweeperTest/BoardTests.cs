using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using MineSweeper;
using NUnit.Framework;

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
    }
}