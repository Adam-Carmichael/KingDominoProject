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
    public class TileTests
    {
        [TestMethod()]
        public void TileTest()
        {
            Tile placeholderTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Assert.IsNotNull(placeholderTile);
            Assert.AreEqual(TileType.Null, placeholderTile.TileType);
        }   
    }
}