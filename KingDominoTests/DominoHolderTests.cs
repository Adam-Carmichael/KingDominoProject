using Microsoft.VisualStudio.TestTools.UnitTesting;
using KingDomino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KingDomino.Tests
{
    [TestClass()]
    public class DominoHolderTests
    {

        [TestMethod()]
        public void DominoHolderTest()
        {
            DominoHolder dominoHolder = new DominoHolder();
            Assert.IsInstanceOfType(dominoHolder, typeof(DominoHolder));
        }

        [TestMethod()]
        public void RandomDominoTest()
        {
            DominoHolder dominoHolder = new DominoHolder();
            Domino randDomino = dominoHolder.RandomDomino();
            Assert.IsNotNull(randDomino);
        }
    }
}