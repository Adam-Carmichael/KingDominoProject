using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KingDomino;

namespace UnitTests
{
    [TestClass]
    public class ViewModelUnitTests : ViewModel
    {
        Domino ChosenDomino = new Domino();

        [TestMethod]
        public void UpdateChosenDominoTest()
        {
            ChosenDomino = CurrentDominos[index];
            OnPropertyChanged("ChosenDomino");
            ShowChosenButtons = Visibility.Visible;
            OnPropertyChanged("ShowChosenButtons");
            ShowButtons = Visibility.Hidden;
            OnPropertyChanged("ShowButtons");
        }
    }
}
