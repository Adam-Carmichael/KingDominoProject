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
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerTest()
        {
            Player testPlayer = new Player();
            Assert.IsNotNull(testPlayer.Board);
        }
    }
}