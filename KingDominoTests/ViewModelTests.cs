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
    public class ViewModelTests
    {
        [TestMethod()]
        public void ViewModelTest()
        {
            ViewModel testVM = new ViewModel();
            Assert.IsNotNull(testVM.PlayerList);
            Assert.IsNotNull(testVM.NextDominos);
            Assert.IsNotNull(testVM.CurrentDominos);
        }

        [TestMethod()]
        public void CreatePlayersTest()
        {
            ViewModel testVM = new ViewModel();
            Assert.AreEqual(4, testVM.PlayerList.Count);
        }

        [TestMethod()]
        public void DisplayChatMessageTest()
        {
            ViewModel testVM = new ViewModel();
            testVM.PlayerList[0].Name = "General Kenobi";
            testVM.DisplayChatMessage(0, "Hello There!");
            Assert.AreEqual("General Kenobi: Hello There!\n", testVM.ChatHistory);
        }

        [TestMethod()]
        public void UpdateChosenDominoTest()
        {
            ViewModel testVM = new ViewModel();
            Tile testTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Domino testDomino = new Domino(testTile, testTile, "Resources/Domino/01.png", 1);
            testVM.CurrentDominos[0] = testDomino;
            testVM.UpdateChosenDomino(0);
            Assert.AreEqual(testDomino, testVM.ChosenDomino);
        }

        [TestMethod()]
        public void UpdateChosenTileTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdatePlacedTileTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SwitchBoardViewTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetBoardTileVisiblityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateScoresTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetCurrentDominosFromNextDominosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateBackFacingDominosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ShowOptionsTest()
        {
            Assert.Fail();
        }
    }
}