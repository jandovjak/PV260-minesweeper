using System;
using FakeItEasy;
using MineSweeper;
using NUnit.Framework;

namespace MineSweeperTest
{
    public class GameTests
    {
        [Test]
        public void CheckLeftClick_GivenTileIsEmpty()
        {
            var game = new Game(new Board(3, 3));
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
            game.LeftClick(1, 1);
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
        }
        
        [Test]
        public void CheckLeftClick_GivenTileIsNumber()
        {
            var game = new Game(new Board(3, 3));
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
            game.Board.GetTile(0, 0).BombsAround = 1;
            game.LeftClick(1, 1);
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
        }
        
        [Test]
        public void CheckLeftClick_GivenTileIsFlag()
        {
            var game = new Game(new Board(3, 3));
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
            game.Board.GetTile(0, 0).IsFlag = true;
            game.LeftClick(1, 1);
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
        }
        
        [Test]
        public void CheckLeftClick_GivenTileIsBomb()
        {
            var game = new Game(new Board(3, 3));
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
            game.Board.GetTile(0, 0).IsBomb = true;
            game.LeftClick(1, 1);
            Assert.AreEqual(GameStatus.Lose, game.GameStatus);
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsEmpty()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsNumber()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsFlag()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsBomb()
        {
            throw new NotImplementedException();
        }
    }
}