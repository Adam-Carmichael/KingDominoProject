using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KingDomino;

namespace KingDomino.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void DominoHolderTest()
        {
            DominoHolder dominos = new DominoHolder();
            Assert.IsNotNull(dominos);
        }

        [TestMethod()]
        public void RandomDominoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DominoHolderGeneratorTest()
        {
            Assert.Fail();
        }
    }
}
{
    [TestClass]
    public class ViewModelUnitTests
    {
        Domino ChosenDomino;
        Domino placeholderTile;
        //ViewModel viewModel = new ViewModel();

        [TestMethod]
        public void UpdateChosenDominoTest()
        {
            //ChosenDomino = CurrentDominos[index];

            //OnPropertyChanged("ChosenDomino");
            //ShowChosenButtons = Visibility.Visible;
            //OnPropertyChanged("ShowChosenButtons");
            //ShowButtons = Visibility.Hidden;
            //OnPropertyChanged("ShowButtons");
            Assert.Fail();
        }
    }
}
