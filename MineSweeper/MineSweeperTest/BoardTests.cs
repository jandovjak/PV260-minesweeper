using System;
using System.Collections;
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
            board.Initialize();
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
        [TestCase(0, 0)]
        [TestCase(4, 4)]
        [TestCase(0, 4)]
        [TestCase(4, 0)]
        public void CheckRevealTile_GivenTileIsBomb_BoardSize5x5(int x, int y)
        {
            var board = board5x5BombsInAllCorners();
            var tile = board.GetTile(x, y);
            var tiles = board.Tiles;
            
            board.RevealTile(x, y);
            
            Assert.IsTrue(tile.IsRevealed);


            int numRevealedTilesAfter = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(1, numRevealedTilesAfter);
        }
        
        [Test]
        [TestCase(0, 1)]
        [TestCase(0, 3)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 3)]
        [TestCase(1, 4)]
        [TestCase(3, 0)]
        [TestCase(3, 1)]
        [TestCase(3, 3)]
        [TestCase(3, 4)]
        [TestCase(4, 1)]
        [TestCase(4, 3)]
        public void CheckRevealTile_GivenTileIsNumber_BoardSize5x5(int x, int y)
        {
            var board = board5x5BombsInAllCorners();
            var tile = board.GetTile(x, y);
            var tiles = board.Tiles;
            
            board.RevealTile(x, y);
            
            Assert.IsTrue(tile.IsRevealed);


            int numRevealedTilesAfter = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(1, numRevealedTilesAfter);
        }
        
        [Test]
        [TestCase(0, 2)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 3)]
        [TestCase(2, 4)]
        public void CheckRevealTile_GivenTileIsEmpty_BoardSize5x5(int x, int y)
        {
            var board = board5x5BombsInAllCorners();
            var tile = board.GetTile(x, y);
            var tiles = board.Tiles;
            
            board.RevealTile(x, y);
            
            Assert.IsFalse(board.GetTile(0, 0).IsRevealed);
            Assert.IsFalse(board.GetTile(4, 4).IsRevealed);
            Assert.IsFalse(board.GetTile(0, 4).IsRevealed);
            Assert.IsFalse(board.GetTile(4, 0).IsRevealed);


            int numRevealedTilesAfter = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(21, numRevealedTilesAfter);
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        public void CheckRevealTile_GivenTileIsEmpty_EmptyBoard5x5(int x, int y)
        {
            var board = emptyBoard3x3();
            board.RevealTile(x, y);
            var numRevealedTilesAfter = board.Tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(9, numRevealedTilesAfter);
        }
        
        [Test]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        [TestCase(2, 3)]
        [TestCase(2, 4)]
        public void CheckRevealTile_GivenTileIsBomb_Board5x5BombsInMiddleRow(int x, int y)
        {
            var board = board5x5BombsInMiddleRow();
            var tiles = board.Tiles;
            
            board.RevealTile(x, y);
            Assert.IsTrue(board.GetTile(x, y).IsRevealed);

            int numRevealedTilesAfter = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(1, numRevealedTilesAfter);
        }
        
        [Test]
        [TestCase(1, 0)]
        [TestCase(3, 0)]
        [TestCase(1, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(3, 2)]
        [TestCase(1, 3)]
        [TestCase(3, 3)]
        [TestCase(1, 4)]
        [TestCase(3, 4)]
        public void CheckRevealTile_GivenTileNextToBomb_Board5x5BombsInMiddleRow(int x, int y)
        {
            var board = board5x5BombsInMiddleRow();
            var tiles = board.Tiles;
            board.RevealTile(x, y);
            Assert.IsTrue(board.GetTile(x, y).IsRevealed);
            int numRevealedTilesAfter = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(1, numRevealedTilesAfter);
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(0, 3)]
        [TestCase(0, 4)]
        public void CheckRevealTile_GivenTileIsEmptyTileInUpperPart_Board5x5BombsInMiddleRow(int x, int y)
        {
            var board = board5x5BombsInMiddleRow();
            var tiles = board.Tiles;
            board.RevealTile(x, y);
            Assert.IsTrue(board.GetTile(x, y).IsRevealed);
            var revealedTilesOnBoard = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(10, revealedTilesOnBoard);
            var revealedTilesOnBoardInUpperPart = tiles.Take(15).Count(tile => tile.IsRevealed);
            Assert.AreEqual(10, revealedTilesOnBoardInUpperPart);
        }
        
        [Test]
        [TestCase(4, 0)]
        [TestCase(4, 1)]
        [TestCase(4, 2)]
        [TestCase(4, 3)]
        [TestCase(4, 4)]
        public void CheckRevealTile_GivenTileIsEmptyTileInLowerPart_Board5x5BombsInMiddleRow(int x, int y)
        {
            var board = board5x5BombsInMiddleRow();
            var tiles = board.Tiles;
            board.RevealTile(x, y);
            Assert.IsTrue(board.GetTile(x, y).IsRevealed);
            var revealedTilesOnBoard = tiles.Count(tile => tile.IsRevealed);
            Assert.AreEqual(10, revealedTilesOnBoard);
            var revealedTilesOnBoardInUpperPart = tiles.Skip(10).Count(tile => tile.IsRevealed);
            Assert.AreEqual(10, revealedTilesOnBoardInUpperPart);
        }
        
        private Board board5x5BombsInAllCorners()
        {
            var board = new Board(5, 5);
            board.GetTile(0, 0).IsBomb = true;
            board.GetTile(1, 1).BombsAround = 1;
            board.GetTile(0, 1).BombsAround = 1;
            board.GetTile(1, 0).BombsAround = 1;
            board.GetTile(4, 4).IsBomb = true;
            board.GetTile(3, 3).BombsAround = 1;
            board.GetTile(3, 4).BombsAround = 1;
            board.GetTile(4, 3).BombsAround = 1;
            board.GetTile(0, 4).IsBomb = true;
            board.GetTile(0, 3).BombsAround = 1;
            board.GetTile(1, 3).BombsAround = 1;
            board.GetTile(1, 4).BombsAround = 1;
            board.GetTile(4, 0).IsBomb = true;
            board.GetTile(3, 0).BombsAround = 1;
            board.GetTile(3, 1).BombsAround = 1;
            board.GetTile(4, 1).BombsAround = 1;
            return board;
        }
        
        private Board board5x5BombsInMiddleRow()
        {
            var board = new Board(5, 5);
            board.GetTile(2, 0).IsBomb = true;
            board.GetTile(1, 0).BombsAround = 2;
            board.GetTile(3, 0).BombsAround = 2;
            board.GetTile(2, 1).IsBomb = true;
            board.GetTile(1, 1).BombsAround = 3;
            board.GetTile(3, 1).BombsAround = 3;
            board.GetTile(2, 2).IsBomb = true;
            board.GetTile(1, 2).BombsAround = 3;
            board.GetTile(3, 2).BombsAround = 3;
            board.GetTile(2, 3).IsBomb = true;
            board.GetTile(1, 3).BombsAround = 3;
            board.GetTile(3, 3).BombsAround = 3;
            board.GetTile(2,4).IsBomb = true;
            board.GetTile(1, 4).BombsAround = 2;
            board.GetTile(3, 4).BombsAround = 2;
            return board;
        }
        
        private Board emptyBoard3x3()
        {
            var board = new Board(3, 3);
            return board;
        }
    }
}