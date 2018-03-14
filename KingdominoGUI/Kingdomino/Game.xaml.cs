using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KingDomino
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private Board player1Board;
        private Domino player1Origin;
        private Board player2Board;
        private Domino player2Origin;
        private Board player3Board;
        private Domino player3Origin;
        private Board player4Board;
        private Domino player4Origin;
        private DominoHolder dominoHolder = new DominoHolder();
        private Domino domino1;
        private Domino domino2;
        private Domino domino3;
        private Domino domino4;
        private Domino domino5;
        private Domino domino6;
        private Domino domino7;
        private Domino domino8;

        private Domino chosen;

        private int roundNumber = 1;
        public Game()
        {
            InitializeComponent();

            MakeBoards();

            Origin.Source = new BitmapImage(new Uri(player1Origin.DominoBack, UriKind.Relative));
            Castle.Source = new BitmapImage(new Uri(player1Origin.Tile1.TileImage, UriKind.Relative));


            domino1 = dominoHolder.RandomDomino();
            Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

            domino2 = dominoHolder.RandomDomino();
            Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

            domino3 = dominoHolder.RandomDomino();
            Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

            domino4 = dominoHolder.RandomDomino();
            Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));


            domino5 = dominoHolder.RandomDomino();
            Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
            Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

            domino6= dominoHolder.RandomDomino();
            Tile1.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
            Tile2.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

            domino7= dominoHolder.RandomDomino();
            Tile1.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
            Tile2.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

            domino8= dominoHolder.RandomDomino();
            Tile1.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
            Tile2.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));
        }

        private void MakeBoards()
        {
            player1Board = new Board("blue");
            player1Origin = player1Board.GetOrigin();
            player2Board = new Board("green");
            player2Origin = player2Board.GetOrigin();
            player3Board = new Board("pink");
            player3Origin = player3Board.GetOrigin();
            player4Board = new Board("yellow");
            player4Origin = player4Board.GetOrigin();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Choose1_Click(object sender, RoutedEventArgs e)
        {
            chosen = domino5;
            SelectedTile1.Source = new BitmapImage(new Uri(chosen.Tile1.TileImage, UriKind.Relative));
            SelectedTile2.Source = new BitmapImage(new Uri(chosen.Tile2.TileImage, UriKind.Relative));
            SelectedDomino.Source = new BitmapImage(new Uri(chosen.DominoBack, UriKind.Relative));
            if (roundNumber < 11)
            {
                roundNumber++;

                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                domino1 = dominoHolder.RandomDomino();
                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                domino2 = dominoHolder.RandomDomino();
                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                domino3 = dominoHolder.RandomDomino();
                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                domino4 = dominoHolder.RandomDomino();
                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if(roundNumber == 11)
            {
                roundNumber++;
                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));
                
                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));
                
                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));
                
                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if(roundNumber == 12)
            {
                roundNumber++;
                Domino1.Source = new BitmapImage(new Uri("", UriKind.Relative));
                
                Domino2.Source = new BitmapImage(new Uri("", UriKind.Relative));
                
                Domino3.Source = new BitmapImage(new Uri("", UriKind.Relative));
                
                Domino4.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
            else
            {

                Tile1.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri("", UriKind.Relative));
                
                Tile3.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri("", UriKind.Relative));
                
                Tile5.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri("", UriKind.Relative));
                
                Tile7.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
        }
        private void Choose2_Click(object sender, RoutedEventArgs e)
        {
            chosen = domino6;
            SelectedTile1.Source = new BitmapImage(new Uri(chosen.Tile1.TileImage, UriKind.Relative));
            SelectedTile2.Source = new BitmapImage(new Uri(chosen.Tile2.TileImage, UriKind.Relative));
            SelectedDomino.Source = new BitmapImage(new Uri(chosen.DominoBack, UriKind.Relative));
            if (roundNumber < 11)
            {
                roundNumber++;

                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                domino1 = dominoHolder.RandomDomino();
                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                domino2 = dominoHolder.RandomDomino();
                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                domino3 = dominoHolder.RandomDomino();
                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                domino4 = dominoHolder.RandomDomino();
                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if (roundNumber == 11)
            {
                roundNumber++;
                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if (roundNumber == 12)
            {
                roundNumber++;
                Domino1.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
            else
            {

                Tile1.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile3.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile5.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile7.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
        }
        private void Choose3_Click(object sender, RoutedEventArgs e)
        {
            chosen = domino7;
            SelectedTile1.Source = new BitmapImage(new Uri(chosen.Tile1.TileImage, UriKind.Relative));
            SelectedTile2.Source = new BitmapImage(new Uri(chosen.Tile2.TileImage, UriKind.Relative));
            SelectedDomino.Source = new BitmapImage(new Uri(chosen.DominoBack, UriKind.Relative));
            if (roundNumber < 11)
            {
                roundNumber++;

                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                domino1 = dominoHolder.RandomDomino();
                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                domino2 = dominoHolder.RandomDomino();
                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                domino3 = dominoHolder.RandomDomino();
                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                domino4 = dominoHolder.RandomDomino();
                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if (roundNumber == 11)
            {
                roundNumber++;
                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if (roundNumber == 12)
            {
                roundNumber++;
                Domino1.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
            else
            {

                Tile1.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile3.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile5.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile7.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
        }
        private void Choose4_Click(object sender, RoutedEventArgs e)
        {
            chosen = domino8;
            SelectedTile1.Source = new BitmapImage(new Uri(chosen.Tile1.TileImage, UriKind.Relative));
            SelectedTile2.Source = new BitmapImage(new Uri(chosen.Tile2.TileImage, UriKind.Relative));
            SelectedDomino.Source = new BitmapImage(new Uri(chosen.DominoBack, UriKind.Relative));
            if (roundNumber < 11)
            {
                roundNumber++;

                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                domino1 = dominoHolder.RandomDomino();
                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                domino2 = dominoHolder.RandomDomino();
                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                domino3 = dominoHolder.RandomDomino();
                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                domino4 = dominoHolder.RandomDomino();
                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if (roundNumber == 11)
            {
                roundNumber++;
                domino5 = domino1;
                Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

                domino6 = domino2;
                Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

                domino7 = domino3;
                Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

                domino8 = domino4;
                Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));


                Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            }
            else if (roundNumber == 12)
            {
                roundNumber++;
                Domino1.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
            else
            {
                
                Tile1.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile3.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile5.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile7.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
        }
    }
}
