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
    public class DominoTests
    {
        [TestMethod()]
        public void DominoTest()
        {
            Tile placeholderTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Domino placeholderDomino = new Domino(placeholderTile, placeholderTile, "Resources/Domino/01.png", 1);
            Assert.AreEqual(placeholderTile, placeholderDomino.Tile1);
            Assert.AreEqual(placeholderTile, placeholderDomino.Tile2);
            Assert.AreEqual(1, placeholderDomino.Number);
        }
    }
}