using System;
using System.Collections.Generic;
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
        public void CheckRightClick_GivenTileIsRevealed()
        {
            var game = new Game(new Board(3, 3));
            game.Board.GetTile(0, 0).IsRevealed = true;
            int tilesFlaggedBefore = game.Board.TilesFlagged;
            int bombsFlaggedBefore = game.Board.BombsFlagged;
            Assert.IsFalse(game.Board.GetTile(0, 0).IsFlag);

            game.RightClick(1, 1);

            Assert.IsFalse(game.Board.GetTile(0, 0).IsFlag);
            Assert.AreEqual(tilesFlaggedBefore, game.Board.TilesFlagged);
            Assert.AreEqual(bombsFlaggedBefore, game.Board.BombsFlagged);
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsNotBombAndIsNotFlag()
        {
            var game = new Game(new Board(3, 3));
            game.Board.GetTile(0, 0).IsBomb = false;
            game.Board.GetTile(0, 0).IsFlag = false;
            int tilesFlaggedBefore = game.Board.TilesFlagged;
            int bombsFlaggedBefore = game.Board.BombsFlagged;
            Assert.IsFalse(game.Board.GetTile(0, 0).IsFlag);

            game.RightClick(1, 1);

            Assert.IsTrue(game.Board.GetTile(0, 0).IsFlag);
            Assert.AreEqual(tilesFlaggedBefore + 1, game.Board.TilesFlagged);
            Assert.AreEqual(bombsFlaggedBefore, game.Board.BombsFlagged);
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsNotBombAndIsFlag()
        {
            var game = new Game(new Board(3, 3));
            game.Board.GetTile(0, 0).IsBomb = false;
            game.Board.GetTile(0, 0).IsFlag = true;
            int tilesFlaggedBefore = game.Board.TilesFlagged;
            int bombsFlaggedBefore = game.Board.BombsFlagged;
            Assert.IsTrue(game.Board.GetTile(0, 0).IsFlag);

            game.RightClick(1, 1);

            Assert.IsFalse(game.Board.GetTile(0, 0).IsFlag);
            Assert.AreEqual(tilesFlaggedBefore - 1, game.Board.TilesFlagged);
            Assert.AreEqual(bombsFlaggedBefore, game.Board.BombsFlagged);
        }
        
        [Test]
        public void CheckRightClick_GivenTileIsBombAndIsNotFlag()
        {
            var game = new Game(new Board(3, 3));
            game.Board.GetTile(0, 0).IsBomb = true;
            game.Board.GetTile(0, 0).IsFlag = false;
            int tilesFlaggedBefore = game.Board.TilesFlagged;
            int bombsFlaggedBefore = game.Board.BombsFlagged;
            Assert.IsFalse(game.Board.GetTile(0, 0).IsFlag);

            game.RightClick(1, 1);

            Assert.IsTrue(game.Board.GetTile(0, 0).IsFlag);
            Assert.AreEqual(tilesFlaggedBefore + 1, game.Board.TilesFlagged);
            Assert.AreEqual(bombsFlaggedBefore + 1, game.Board.BombsFlagged);
        }

        [Test]
        public void CheckRightClick_GivenTileIsBombAndIsFlag()
        {
            var game = new Game(new Board(3, 3));
            game.Board.GetTile(0, 0).IsBomb = true;
            game.Board.GetTile(0, 0).IsFlag = true;
            int tilesFlaggedBefore = game.Board.TilesFlagged;
            int bombsFlaggedBefore = game.Board.BombsFlagged;
            Assert.IsTrue(game.Board.GetTile(0, 0).IsFlag);

            game.RightClick(1, 1);

            Assert.IsFalse(game.Board.GetTile(0, 0).IsFlag);
            Assert.AreEqual(tilesFlaggedBefore - 1, game.Board.TilesFlagged);
            Assert.AreEqual(bombsFlaggedBefore - 1, game.Board.BombsFlagged);
        }

        [Test]
        public void CheckRightClick_PlayerFlagsAllBombs_ButShouldNotWin()
        {
            var tiles = createNineTilesFirstThreeAreBombs();

            var game = new Game(new Board(3, 3, tiles));
            Assert.AreEqual(3, game.Board.BombsAmount);
            Assert.AreEqual(0, game.Board.TilesFlagged);
            Assert.AreEqual(0, game.Board.BombsFlagged);

            game.RightClick(1, 1);
            game.RightClick(1, 2);
            game.RightClick(1, 3);

            Assert.IsTrue(game.AreAllBombsFlagged());
            Assert.AreEqual(GameStatus.Win, game.GameStatus);
        }

        [Test]
        public void CheckRightClick_PlayerFlagsAllBombs_ShouldWin()
        {
            var tiles = createNineTilesFirstThreeAreBombs();

            var game = new Game(new Board(3, 3, tiles));
            Assert.AreEqual(3, game.Board.BombsAmount);
            Assert.AreEqual(0, game.Board.TilesFlagged);
            Assert.AreEqual(0, game.Board.BombsFlagged);

            game.RightClick(1, 1);
            game.RightClick(1, 2);
            game.RightClick(3, 3);
            game.RightClick(1, 3);

            Assert.IsFalse(game.AreAllBombsFlagged());
            Assert.AreEqual(GameStatus.Playing, game.GameStatus);
        }

        private List<ITile> createNineTilesFirstThreeAreBombs()
        {
            var tiles = new List<ITile>();
            for (int i = 0; i < 9; i++)
                tiles.Add(new Tile());

            tiles[0].IsBomb = true;
            tiles[1].IsBomb = true;
            tiles[2].IsBomb = true;

            return tiles;
        }
    }
}