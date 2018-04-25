using Microsoft.VisualStudio.TestTools.UnitTesting;
using KingDomino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void BoardTest()
        {
            Board testBoard = new Board("pink");
            Assert.AreEqual(TileType.Origin, testBoard.PlayBoard[2][2].TileType);
        }

        [TestMethod()]
        public void CalculateScoreTest()
        {
            Board testBoard = new Board("green");
            Tile testTile = new Tile("Resources/Misc/logo.png", TileType.Null, 1);
            testBoard.PlayBoard[1][2] = testTile;
            testBoard.PlayBoard[1][1] = testTile;
            Assert.AreEqual(4, testBoard.CalculateScore());
        }

        [TestMethod()]
        public void GetOriginTest()
        {
            Board testBoard = new Board("green");
            Assert.AreEqual(testBoard.PlayBoard[2][2].TileType, testBoard.GetOrigin().TileType);
        }

        [TestMethod()]
        public void AddTest()
        {
            Board testBoard = new Board("yellow");
            Assert.IsNull(testBoard.PlayBoard[1][2]);
            Tile testTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            testBoard.Add(testTile, 1, 2);
            Assert.IsNotNull(testBoard.PlayBoard[1][2]);
        }
    }
}