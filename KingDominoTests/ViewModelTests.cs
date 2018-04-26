using Microsoft.VisualStudio.TestTools.UnitTesting;
using KingDomino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
            ViewModel testVM = new ViewModel();
            Tile testTile1 = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Tile testTile2 = new Tile("Resources/Misc/logo.png", TileType.Null, 1);
            Domino testDomino = new Domino(testTile1, testTile2, "Resources/Domino/01.png", 1);
            testVM.ChosenDomino = testDomino;
            testVM.UpdateChosenTile(0);
            Assert.AreEqual(1, testVM.ChosenTile.TileCrown);

            testVM.UpdateChosenTile(1);
            Assert.AreEqual(0, testVM.ChosenTile.TileCrown);
        }

        [TestMethod()]
        public void UpdatePlacedTileTest()
        {
            ViewModel testVM = new ViewModel();
            Tile testTile1 = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Tile testTile2 = new Tile("Resources/Misc/logo.png", TileType.Null, 1);
            Domino testDomino = new Domino(testTile1, testTile2, "Resources/Domino/01.png", 1);
        
            testVM.ChosenTile = testTile1;
            testVM.UpdatePlacedTile(1, 2);
            Assert.AreEqual(testTile1, testVM.CurrentBoard.PlayBoard[1][2]);

            testVM.ChosenDomino = testDomino;
            testVM.UpdatePlacedTile(1, 1);
            Assert.AreEqual(testTile2, testVM.ChosenTile);

            testVM.UpdatePlacedTile(1, 3);
            testVM.UpdatePlacedTile(0, 2);
            Assert.AreEqual(testTile1, testVM.ChosenTile);

            testVM.UpdatePlacedTile(2, 1);
            Assert.AreEqual(testTile1, testVM.CurrentBoard.PlayBoard[2][1]);
        }

        [TestMethod()]
        public void RotateDominoSelectionRound10Test()
        {
            ViewModel testVM = new ViewModel();
            Tile testTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Domino testDomino = new Domino(testTile, testTile, "Resources/Domino/01.png", 1);
            testVM.ChosenTile = testTile;
            testVM.ChosenDomino = testDomino;

            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);


            Domino[] testNextDominos = new Domino[4];
            testVM.NextDominos.CopyTo(testNextDominos,0);
            testVM.UpdatePlacedTile(2, 1);
            Domino[] testCurrentDominos = new Domino[4];
            testVM.CurrentDominos.CopyTo(testCurrentDominos, 0);
            Assert.AreEqual(testNextDominos[0].Number, testCurrentDominos[0].Number);
        }
        [TestMethod()]
        public void RotateDominoSelectionRound11Test()
        {
            ViewModel testVM = new ViewModel();
            Tile testTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            Domino testDomino = new Domino(testTile, testTile, "Resources/Domino/01.png", 1);
            testVM.ChosenTile = testTile;
            testVM.ChosenDomino = testDomino;

            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);
            testVM.UpdatePlacedTile(2, 1);

            Domino[] testNextDominos = new Domino[4];
            testVM.NextDominos.CopyTo(testNextDominos, 0);
            testVM.UpdatePlacedTile(2, 1);
            Assert.IsNull(testNextDominos[0]);
            //Domino[] testCurrentDominos = new Domino[4];
            //testVM.CurrentDominos.CopyTo(testCurrentDominos, 0);
            //Assert.AreEqual(testNextDominos[0].Number, testCurrentDominos[0].Number);
        }

        [TestMethod()]
        public void SwitchBoardViewTest()
        {
            ViewModel testVM = new ViewModel();
            testVM.CurrentBoard = testVM.PlayerList[0].Board;
            testVM.SwitchBoardView(1);
            Assert.AreEqual(testVM.CurrentBoard, testVM.PlayerList[1].Board);
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