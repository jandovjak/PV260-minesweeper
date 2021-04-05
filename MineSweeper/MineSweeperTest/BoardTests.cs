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
    }
}