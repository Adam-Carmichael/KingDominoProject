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
        private Tile player1Origin;

        private Board player2Board;
        private Tile player2Origin;

        private Board player3Board;
        private Tile player3Origin;

        private Board player4Board;
        private Tile player4Origin;

        private DominoHolder dominoHolder = new DominoHolder();

        private Domino domino1;
        private Domino domino2;
        private Domino domino3;
        private Domino domino4;
        private Domino domino5;
        private Domino domino6;
        private Domino domino7;
        private Domino domino8;

        private int pick = 0;
        private int roundNumber = 1;
        private int lastIpos = 0;       // Column position of last tile placed (board is at this point in time arranged by i columns and j rows)
        private int lastJpos = 0;       // Row position of last tile placed
        public Game()
        {
            InitializeComponent();

            MakeBoards();

            MakeInitialDominos();
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
            RefreshBoard(player1Board);
        }

        // This method generates a players board. Can be used when switching between player boards
        private void RefreshBoard(Board board)
        {
            CalculateScores();
            Score.Text = "Score: " + board.Score;
            OneOne.Source = null;
            OneTwo.Source = null;
            OneThree.Source = null;
            OneFour.Source = null;
            OneFive.Source = null;
            TwoOne.Source = null;
            TwoTwo.Source = null;
            TwoThree.Source = null;
            TwoFour.Source = null;
            TwoFive.Source = null;
            ThreeOne.Source = null;
            ThreeTwo.Source = null;
            ThreeThree.Source = null;
            ThreeFour.Source = null;
            ThreeFive.Source = null;
            FourOne.Source = null;
            FourTwo.Source = null;
            FourThree.Source = null;
            FourFour.Source = null;
            FourFive.Source = null;
            FiveOne.Source = null;
            FiveTwo.Source = null;
            FiveThree.Source = null;
            FiveFour.Source = null;
            FiveFive.Source = null;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board.PlayBoard[i,j] != null)
                    {
                        if(i == 0)
                        {
                            if(j == 0)
                            {
                                OneOne.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if(j == 1)
                            {
                                OneTwo.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if(j == 2)
                            {
                                OneThree.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if(j == 3)
                            {
                                OneFour.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 4)
                            {
                                OneFive.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                        }
                        if(i == 1)
                        {
                            if (j == 0)
                            {
                                TwoOne.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 1)
                            {
                                TwoTwo.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 2)
                            {
                                TwoThree.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 3)
                            {
                                TwoFour.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 4)
                            {
                                TwoFive.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                        }
                        if(i == 2)
                        {
                            if (j == 0)
                            {
                                ThreeOne.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 1)
                            {
                                ThreeTwo.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 2)
                            {
                                ThreeThree.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 3)
                            {
                                ThreeFour.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 4)
                            {
                                ThreeFive.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                        }
                        if(i == 3)
                        {
                            if (j == 0)
                            {
                                FourOne.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 1)
                            {
                                FourTwo.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 2)
                            {
                                FourThree.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 3)
                            {
                                FourFour.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 4)
                            {
                                FourFive.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                        }
                        if(i == 4)
                        {
                            if (j == 0)
                            {
                                FiveOne.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 1)
                            {
                                FiveTwo.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 2)
                            {
                                FiveThree.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 3)
                            {
                                FiveFour.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                            if (j == 4)
                            {
                                FiveFive.Source = new BitmapImage(new Uri(board.PlayBoard[i, j].TileImage, UriKind.Relative));
                            }
                        }
                    }
                }
            }
        }

        // Refreshs the four starting dominos and flips the next ones
        private void RefreshSelectionDomionos()
        {

            //Create new backfacing dominos and set the old ones as the new tiles
            if (roundNumber <= 10)
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

            //Removes the backfacing dominos once there is no more dominos left in the DominoHolder and then sets the last dominos as the new tiles
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


                Domino1.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino3.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Domino4.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }

            //Removes the tiles now that there is no more tiles or dominos
            else if (roundNumber == 13)
            {
                roundNumber++;
                Tile1.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile2.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile3.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile4.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile5.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile6.Source = new BitmapImage(new Uri("", UriKind.Relative));

                Tile7.Source = new BitmapImage(new Uri("", UriKind.Relative));
                Tile8.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
            else
            {
                
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
        }

        //Sets up the inital setup
        private void MakeInitialDominos()
        {
            ThreeThree.Source = new BitmapImage(new Uri(player1Origin.TileImage, UriKind.Relative));

            HideOptions();
            HideTileSelectionButtons();

            //Set up the first backfacing dominos
            domino1 = dominoHolder.RandomDomino();
            Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));

            domino2 = dominoHolder.RandomDomino();
            Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));

            domino3 = dominoHolder.RandomDomino();
            Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));

            domino4 = dominoHolder.RandomDomino();
            Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));

            //Set up the first tiles
            domino5 = dominoHolder.RandomDomino();
            Tile1.Source = new BitmapImage(new Uri(domino5.Tile1.TileImage, UriKind.Relative));
            Tile2.Source = new BitmapImage(new Uri(domino5.Tile2.TileImage, UriKind.Relative));

            domino6 = dominoHolder.RandomDomino();
            Tile3.Source = new BitmapImage(new Uri(domino6.Tile1.TileImage, UriKind.Relative));
            Tile4.Source = new BitmapImage(new Uri(domino6.Tile2.TileImage, UriKind.Relative));

            domino7 = dominoHolder.RandomDomino();
            Tile5.Source = new BitmapImage(new Uri(domino7.Tile1.TileImage, UriKind.Relative));
            Tile6.Source = new BitmapImage(new Uri(domino7.Tile2.TileImage, UriKind.Relative));

            domino8 = dominoHolder.RandomDomino();
            Tile7.Source = new BitmapImage(new Uri(domino8.Tile1.TileImage, UriKind.Relative));
            Tile8.Source = new BitmapImage(new Uri(domino8.Tile2.TileImage, UriKind.Relative));
        }

        // Sets the image where the button where the user clicked as one of their tiles of their selected domino.
        // It will set the first tile if it is their first pick and their second tile if it is their second pick.
        private void ChooseSpot_Click(object sender, RoutedEventArgs e)
        {
            if(sender.GetType().IsVisible)
            {
                if (pick == 0)
                {
                    if (sender.Equals(OneOneButton))
                    {
                        OneOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 0, 0);
                        lastIpos = 0;
                        lastJpos = 0;
                    }
                    else if (sender.Equals(OneTwoButton))
                    {
                        OneTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 0, 1);
                        lastIpos = 0;
                        lastJpos = 1;
                    }
                    else if (sender.Equals(OneThreeButton))
                    {
                        OneThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 0, 2);
                        lastIpos = 0;
                        lastJpos = 2;
                    }
                    else if (sender.Equals(OneFourButton))
                    {
                        OneFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 0, 3);
                        lastIpos = 0;
                        lastJpos = 3;
                    }
                    else if (sender.Equals(OneFiveButton))
                    {
                        OneFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 0, 4);
                        lastIpos = 0;
                        lastJpos = 4;
                    }
                    else if (sender.Equals(TwoOneButton))
                    {
                        TwoOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 1, 0);
                        lastIpos = 1;
                        lastJpos = 0;
                    }
                    else if (sender.Equals(TwoTwoButton))
                    {
                        TwoTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 1, 1);
                        lastIpos = 1;
                        lastJpos = 1;
                    }
                    else if (sender.Equals(TwoThreeButton))
                    {
                        TwoThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 1, 2);
                        lastIpos = 1;
                        lastJpos = 2;
                    }
                    else if (sender.Equals(TwoFourButton))
                    {
                        TwoFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 1, 3);
                        lastIpos = 1;
                        lastJpos = 3;
                    }
                    else if (sender.Equals(TwoFiveButton))
                    {
                        TwoFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 1, 4);
                        lastIpos = 1;
                        lastJpos = 4;
                    }
                    else if (sender.Equals(ThreeOneButton))
                    {
                        ThreeOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 2, 0);
                        lastIpos = 2;
                        lastJpos = 0;
                    }
                    else if (sender.Equals(ThreeTwoButton))
                    {
                        ThreeTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 2, 1);
                        lastIpos = 2;
                        lastJpos = 1;
                    }
                    else if (sender.Equals(ThreeThreeButton))
                    {
                        ThreeThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 2, 2);
                        lastIpos = 2;
                        lastJpos = 2;
                    }
                    else if (sender.Equals(ThreeFourButton))
                    {
                        ThreeFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 2, 3);
                        lastIpos = 2;
                        lastJpos = 3;
                    }
                    else if (sender.Equals(ThreeFiveButton))
                    {
                        ThreeFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 2, 4);
                        lastIpos = 2;
                        lastJpos = 4;
                    }
                    else if (sender.Equals(FourOneButton))
                    {
                        FourOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 3, 0);
                        lastIpos = 3;
                        lastJpos = 0;
                    }
                    else if (sender.Equals(FourTwoButton))
                    {
                        FourTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 3, 1);
                        lastIpos = 3;
                        lastJpos = 1;
                    }
                    else if (sender.Equals(FourThreeButton))
                    {
                        FourThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 3, 2);
                        lastIpos = 3;
                        lastJpos = 2;
                    }
                    else if (sender.Equals(FourFourButton))
                    {
                        FourFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 3, 3);
                        lastIpos = 3;
                        lastJpos = 3;
                    }
                    else if (sender.Equals(FourFiveButton))
                    {
                        FourFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 3, 4);
                        lastIpos = 3;
                        lastJpos = 4;
                    }
                    else if (sender.Equals(FiveOneButton))
                    {
                        FiveOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 4, 0);
                        lastIpos = 4;
                        lastJpos = 0;
                    }
                    else if (sender.Equals(FiveTwoButton))
                    {
                        FiveTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 4, 1);
                        lastIpos = 4;
                        lastJpos = 1;
                    }
                    else if (sender.Equals(FiveThreeButton))
                    {
                        FiveThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 4, 2);
                        lastIpos = 4;
                        lastJpos = 2;
                    }
                    else if (sender.Equals(FiveFourButton))
                    {
                        FiveFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 4, 3);
                        lastIpos = 4;
                        lastJpos = 3;
                    }
                    else if (sender.Equals(FiveFiveButton))
                    {
                        FiveFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile1, 4, 4);
                        lastIpos = 4;
                        lastJpos = 4;
                    }
                    pick++;
                    HideOptions();
                    ShowOptions();
                }
                else if (pick == 1)
                {
                    if (sender.Equals(OneOneButton))
                    {
                        OneOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 0, 0);
                    }
                    else if (sender.Equals(OneTwoButton))
                    {
                        OneTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 0, 1);
                    }
                    else if (sender.Equals(OneThreeButton))
                    {
                        OneThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 0, 2);
                    }
                    else if (sender.Equals(OneFourButton))
                    {
                        OneFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 0, 3);
                    }
                    else if (sender.Equals(OneFiveButton))
                    {
                        OneFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 0, 4);
                    }
                    else if (sender.Equals(TwoOneButton))
                    {
                        TwoOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 1, 0);
                    }
                    else if (sender.Equals(TwoTwoButton))
                    {
                        TwoTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 1, 1);
                    }
                    else if (sender.Equals(TwoThreeButton))
                    {
                        TwoThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 1, 2);
                    }
                    else if (sender.Equals(TwoFourButton))
                    {
                        TwoFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 1, 3);
                    }
                    else if (sender.Equals(TwoFiveButton))
                    {
                        TwoFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 1, 4);
                    }
                    else if (sender.Equals(ThreeOneButton))
                    {
                        ThreeOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 2, 0);
                    }
                    else if (sender.Equals(ThreeTwoButton))
                    {
                        ThreeTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 2, 1);
                    }
                    else if (sender.Equals(ThreeThreeButton))
                    {
                        ThreeThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 2, 2);
                    }
                    else if (sender.Equals(ThreeFourButton))
                    {
                        ThreeFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 2, 3);
                    }
                    else if (sender.Equals(ThreeFiveButton))
                    {
                        ThreeFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 2, 4);
                    }
                    else if (sender.Equals(FourOneButton))
                    {
                        FourOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 3, 0);
                    }
                    else if (sender.Equals(FourTwoButton))
                    {
                        FourTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 3, 1);
                    }
                    else if (sender.Equals(FourThreeButton))
                    {
                        FourThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 3, 2);
                    }
                    else if (sender.Equals(FourFourButton))
                    {
                        FourFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 3, 3);
                    }
                    else if (sender.Equals(FourFiveButton))
                    {
                        FourFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 3, 4);
                    }
                    else if (sender.Equals(FiveOneButton))
                    {
                        FiveOne.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 4, 0);
                    }
                    else if (sender.Equals(FiveTwoButton))
                    {
                        FiveTwo.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 4, 1);
                    }
                    else if (sender.Equals(FiveThreeButton))
                    {
                        FiveThree.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 4, 2);
                    }
                    else if (sender.Equals(FiveFourButton))
                    {
                        FiveFour.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 4, 3);
                    }
                    else if (sender.Equals(FiveFiveButton))
                    {
                        FiveFive.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                        player1Board.Add(player1Board.chosen.Tile2, 4, 4);
                    }
                    pick++;
                }
                if (pick == 2)
                {
                    HideOptions();
                    ShowSelectDominoButtons();
                    RefreshSelectionDomionos();
                    RefreshBoard(player1Board);
                    ShowBoardButtons();
                    pick = 0;
                }
            }
        }

        //Shows the options where to place their tile
        private void ShowOptions()
        {
            HideOptions();
            RefreshBoard(player1Board);
            HideBoardButtons();
            if (pick == 0)
            {
                // Up or Down button visibility
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        if (player1Board.PlayBoard[i, j] != null)
                        {
                            if (player1Board.PlayBoard[i, j + 1] == null && (player1Board.PlayBoard[i, j].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, j].TileType.Equals("Origin")))
                            {
                                if (i == 0)
                                {
                                    if (j + 1 == 2)
                                    {
                                        OneThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 3)
                                    {
                                        OneFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 4)
                                    {
                                        OneFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 1)
                                {
                                    if (j + 1 == 2)
                                    {
                                        TwoThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 3)
                                    {
                                        TwoFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 4)
                                    {
                                        TwoFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 2)
                                {
                                    if (j + 1 == 2)
                                    {
                                        ThreeThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 3)
                                    {
                                        ThreeFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 4)
                                    {
                                        ThreeFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 3)
                                {
                                    if (j + 1 == 2)
                                    {
                                        FourThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 3)
                                    {
                                        FourFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 4)
                                    {
                                        FourFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 4)
                                {
                                    if (j + 1 == 2)
                                    {
                                        FiveThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 3)
                                    {
                                        FiveFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j + 1 == 4)
                                    {
                                        FiveFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                            }
                            if (player1Board.PlayBoard[i, j - 1] == null && (player1Board.PlayBoard[i, j].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, j].TileType.Equals("Origin")))
                            {
                                if (i == 0)
                                {
                                    if (j - 1 == 0)
                                    {
                                        OneOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 1)
                                    {
                                        OneTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 2)
                                    {
                                        OneThreeButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 1)
                                {
                                    if (j - 1 == 0)
                                    {
                                        TwoOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 1)
                                    {
                                        TwoTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 2)
                                    {
                                        TwoThreeButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 2)
                                {
                                    if (j - 1 == 0)
                                    {
                                        ThreeOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 1)
                                    {
                                        ThreeTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 2)
                                    {
                                        ThreeThreeButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 3)
                                {
                                    if (j - 1 == 0)
                                    {
                                        FourOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 1)
                                    {
                                        FourTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 2)
                                    {
                                        FourThreeButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i == 4)
                                {
                                    if (j - 1 == 0)
                                    {
                                        FiveOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 1)
                                    {
                                        FiveTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j - 1 == 2)
                                    {
                                        FiveThreeButton.Visibility = Visibility.Visible;
                                    }
                                }
                            }
                        }
                    }
                }
                // Left or Right button visibility
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (player1Board.PlayBoard[i, j] != null)
                        {
                            //Right button visibility
                            if (player1Board.PlayBoard[i + 1, j] == null && (player1Board.PlayBoard[i, j].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, j].TileType.Equals("Origin")))
                            {
                                if (i + 1 == 2)
                                {
                                    if (j == 0)
                                    {
                                        ThreeOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        ThreeTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        ThreeThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        ThreeFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        ThreeFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i + 1 == 3)
                                {
                                    if (j == 0)
                                    {
                                        FourOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        FourTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        FourThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        FourFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        FourFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i + 1 == 4)
                                {
                                    if (j == 0)
                                    {
                                        FiveOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        FiveTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        FiveThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        FiveFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        FiveFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                            }
                            //Left button visibility
                            if (player1Board.PlayBoard[i - 1, j] == null && (player1Board.PlayBoard[i, j].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, j].TileType.Equals("Origin")))
                            {
                                if (i - 1 == 0)
                                {
                                    if (j == 0)
                                    {
                                        OneOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        OneTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        OneThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        OneFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        OneFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i - 1 == 1)
                                {
                                    if (j == 0)
                                    {
                                        TwoOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        TwoTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        TwoThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        TwoFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        TwoFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i - 1 == 2)
                                {
                                    if (j == 0)
                                    {
                                        ThreeOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        ThreeTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        ThreeThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        ThreeFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        ThreeFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                                if (i - 1 == 3)
                                {
                                    if (j == 0)
                                    {
                                        FourOneButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 1)
                                    {
                                        FourTwoButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 2)
                                    {
                                        FourThreeButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 3)
                                    {
                                        FourFourButton.Visibility = Visibility.Visible;
                                    }
                                    if (j == 4)
                                    {
                                        FourFiveButton.Visibility = Visibility.Visible;
                                    }
                                }
                            }
                        }
                    }
                }
                for(int i = 0; i < 4; i++)
                {
                    if (player1Board.PlayBoard[4, i] != null)
                    {
                        if (player1Board.PlayBoard[4, i + 1] == null && (player1Board.PlayBoard[4, i].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[4, i].TileType.Equals("Origin")))
                        {
                            if(i == 0)
                            {
                                FiveTwoButton.Visibility = Visibility.Visible;
                            }
                            if (i == 1)
                            {
                                FiveThreeButton.Visibility = Visibility.Visible;
                            }
                            if (i == 2)
                            {
                                FiveFourButton.Visibility = Visibility.Visible;
                            }
                            if (i == 3)
                            {
                                FiveFiveButton.Visibility = Visibility.Visible;
                            }
                        }
                        if (player1Board.PlayBoard[3, i] == null && (player1Board.PlayBoard[4, i].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[4, i].TileType.Equals("Origin")))
                        {
                            if (i == 0)
                            {
                                FourOneButton.Visibility = Visibility.Visible;
                            }
                            if (i == 1)
                            {
                                FourTwoButton.Visibility = Visibility.Visible;
                            }
                            if (i == 2)
                            {
                                FourThreeButton.Visibility = Visibility.Visible;
                            }
                            if (i == 3)
                            {
                                FourFourButton.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    if (player1Board.PlayBoard[i, 4] != null)
                    {
                        if (player1Board.PlayBoard[i + 1, 4] == null && (player1Board.PlayBoard[i, 4].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, 4].TileType.Equals("Origin")))
                        {
                            if (i == 0)
                            {
                                TwoFiveButton.Visibility = Visibility.Visible;
                            }
                            if (i == 1)
                            {
                                ThreeFiveButton.Visibility = Visibility.Visible;
                            }
                            if (i == 2)
                            {
                                FourFiveButton.Visibility = Visibility.Visible;
                            }
                            if (i == 3)
                            {
                                FiveFiveButton.Visibility = Visibility.Visible;
                            }
                        }
                        if (player1Board.PlayBoard[i, 3] == null && (player1Board.PlayBoard[i, 4].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, 4].TileType.Equals("Origin")))
                        {
                            if (i == 0)
                            {
                                OneFourButton.Visibility = Visibility.Visible;
                            }
                            if (i == 1)
                            {
                                TwoFourButton.Visibility = Visibility.Visible;
                            }
                            if (i == 2)
                            {
                                ThreeFourButton.Visibility = Visibility.Visible;
                            }
                            if (i == 3)
                            {
                                FourFourButton.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                for (int i = 1; i < 4; i++)
                {
                    if (player1Board.PlayBoard[4, i] != null)
                    {
                        if (player1Board.PlayBoard[4, i - 1] == null && (player1Board.PlayBoard[4, i].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[4, i].TileType.Equals("Origin")))
                        {
                            if (i == 1)
                            {
                                FiveOneButton.Visibility = Visibility.Visible;
                            }
                            if (i == 2)
                            {
                                FiveTwoButton.Visibility = Visibility.Visible;
                            }
                            if (i == 3)
                            {
                                FiveThreeButton.Visibility = Visibility.Visible;
                            }
                            if (i == 4)
                            {
                                FiveFourButton.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    if (player1Board.PlayBoard[i, 4] != null)
                    {
                        if (player1Board.PlayBoard[i - 1, 4] == null && (player1Board.PlayBoard[i, 4].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[i, 4].TileType.Equals("Origin")))
                        {
                            if (i == 1)
                            {
                                OneFiveButton.Visibility = Visibility.Visible;
                            }
                            if (i == 2)
                            {
                                TwoFiveButton.Visibility = Visibility.Visible;
                            }
                            if (i == 3)
                            {
                                ThreeFiveButton.Visibility = Visibility.Visible;
                            }
                            if (i == 4)
                            {
                                FourFiveButton.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                if (player1Board.PlayBoard[4, 4] != null)
                {
                    if (player1Board.PlayBoard[4, 3] == null && (player1Board.PlayBoard[4, 4].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[4, 4].TileType.Equals("Origin")))
                    {
                        FiveFourButton.Visibility = Visibility.Visible;
                    }
                    if (player1Board.PlayBoard[3, 4] == null && (player1Board.PlayBoard[4, 4].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[4, 4].TileType.Equals("Origin")))
                    {
                        FourFiveButton.Visibility = Visibility.Visible;
                    }
                }
                if (player1Board.PlayBoard[0, 0] != null)
                {
                    if (player1Board.PlayBoard[1, 0] == null && (player1Board.PlayBoard[0, 0].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[0, 0].TileType.Equals("Origin")))
                    {
                        TwoOneButton.Visibility = Visibility.Visible;
                    }
                    if (player1Board.PlayBoard[0, 1] == null && (player1Board.PlayBoard[0, 0].TileType.Equals(player1Board.chosen.Tile1.TileType) || player1Board.PlayBoard[0, 0].TileType.Equals("Origin")))
                    {
                        OneTwoButton.Visibility = Visibility.Visible;
                    }
                }
            }
            if (pick == 1)
            {
                if (lastIpos + 1 <= 4 && player1Board.PlayBoard[lastIpos + 1, lastJpos] == null)
                {
                    int i = lastIpos + 1;
                    int j = lastJpos;

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            OneOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            OneTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            OneThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            OneFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            OneFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 1)
                    {
                        if (j == 0)
                        {
                            TwoOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            TwoTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            TwoThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            TwoFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            TwoFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 2)
                    {
                        if (j == 0)
                        {
                            ThreeOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            ThreeTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            ThreeThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            ThreeFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            ThreeFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 3)
                    {
                        if (j == 0)
                        {
                            FourOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FourTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FourThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FourFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FourFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 4)
                    {
                        if (j == 0)
                        {
                            FiveOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FiveTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FiveThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FiveFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FiveFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                }

                if (lastIpos - 1 >= 0 && player1Board.PlayBoard[lastIpos - 1, lastJpos] == null)
                {
                    int i = lastIpos - 1;
                    int j = lastJpos;

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            OneOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            OneTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            OneThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            OneFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            OneFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 1)
                    {
                        if (j == 0)
                        {
                            TwoOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            TwoTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            TwoThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            TwoFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            TwoFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 2)
                    {
                        if (j == 0)
                        {
                            ThreeOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            ThreeTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            ThreeThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            ThreeFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            ThreeFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 3)
                    {
                        if (j == 0)
                        {
                            FourOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FourTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FourThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FourFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FourFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 4)
                    {
                        if (j == 0)
                        {
                            FiveOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FiveTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FiveThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FiveFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FiveFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                }

                if (lastJpos + 1 <= 4 && player1Board.PlayBoard[lastIpos, lastJpos + 1] == null)
                {
                    int i = lastIpos;
                    int j = lastJpos + 1;

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            OneOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            OneTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            OneThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            OneFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            OneFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 1)
                    {
                        if (j == 0)
                        {
                            TwoOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            TwoTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            TwoThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            TwoFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            TwoFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 2)
                    {
                        if (j == 0)
                        {
                            ThreeOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            ThreeTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            ThreeThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            ThreeFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            ThreeFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 3)
                    {
                        if (j == 0)
                        {
                            FourOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FourTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FourThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FourFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FourFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 4)
                    {
                        if (j == 0)
                        {
                            FiveOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FiveTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FiveThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FiveFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FiveFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                }

                if (lastJpos - 1 >= 0 && player1Board.PlayBoard[lastIpos, lastJpos - 1] == null)
                {
                    int i = lastIpos;
                    int j = lastJpos - 1;

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            OneOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            OneTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            OneThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            OneFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            OneFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 1)
                    {
                        if (j == 0)
                        {
                            TwoOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            TwoTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            TwoThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            TwoFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            TwoFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 2)
                    {
                        if (j == 0)
                        {
                            ThreeOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            ThreeTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            ThreeThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            ThreeFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            ThreeFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 3)
                    {
                        if (j == 0)
                        {
                            FourOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FourTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FourThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FourFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FourFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                    if (i == 4)
                    {
                        if (j == 0)
                        {
                            FiveOneButton.Visibility = Visibility.Visible;
                        }
                        if (j == 1)
                        {
                            FiveTwoButton.Visibility = Visibility.Visible;
                        }
                        if (j == 2)
                        {
                            FiveThreeButton.Visibility = Visibility.Visible;
                        }
                        if (j == 3)
                        {
                            FiveFourButton.Visibility = Visibility.Visible;
                        }
                        if (j == 4)
                        {
                            FiveFiveButton.Visibility = Visibility.Visible;
                        }
                    }
                }
            }

            if(!(OneOneButton.IsVisible || OneTwoButton.IsVisible || OneThreeButton.IsVisible || OneFourButton.IsVisible || OneFiveButton.IsVisible ||
                TwoOneButton.IsVisible || TwoTwoButton.IsVisible || TwoThreeButton.IsVisible || TwoFourButton.IsVisible || TwoFiveButton.IsVisible ||
                    ThreeOneButton.IsVisible || ThreeTwoButton.IsVisible || ThreeThreeButton.IsVisible || ThreeFourButton.IsVisible || ThreeFiveButton.IsVisible ||
                FourOneButton.IsVisible || FourTwoButton.IsVisible || FourThreeButton.IsVisible || FourFourButton.IsVisible || FourFiveButton.IsVisible ||
                FiveOneButton.IsVisible || FiveTwoButton.IsVisible || FiveThreeButton.IsVisible || FiveFourButton.IsVisible || FiveFiveButton.IsVisible))
            {
                ShowSelectDominoButtons();
                RefreshSelectionDomionos();
                RefreshBoard(player1Board);
                ShowBoardButtons();
                pick = 0;
            }
        }
        private void HideOptions()
        {
            //Hide Buttons
            OneOneButton.Visibility = Visibility.Hidden;
            OneTwoButton.Visibility = Visibility.Hidden;
            OneThreeButton.Visibility = Visibility.Hidden;
            OneFourButton.Visibility = Visibility.Hidden;
            OneFiveButton.Visibility = Visibility.Hidden;
            TwoOneButton.Visibility = Visibility.Hidden;
            TwoTwoButton.Visibility = Visibility.Hidden;
            TwoThreeButton.Visibility = Visibility.Hidden;
            TwoFourButton.Visibility = Visibility.Hidden;
            TwoFiveButton.Visibility = Visibility.Hidden;
            ThreeOneButton.Visibility = Visibility.Hidden;
            ThreeTwoButton.Visibility = Visibility.Hidden;
            ThreeThreeButton.Visibility = Visibility.Hidden;
            ThreeFourButton.Visibility = Visibility.Hidden;
            ThreeFiveButton.Visibility = Visibility.Hidden;
            FourOneButton.Visibility = Visibility.Hidden;
            FourTwoButton.Visibility = Visibility.Hidden;
            FourThreeButton.Visibility = Visibility.Hidden;
            FourFourButton.Visibility = Visibility.Hidden;
            FourFiveButton.Visibility = Visibility.Hidden;
            FiveOneButton.Visibility = Visibility.Hidden;
            FiveTwoButton.Visibility = Visibility.Hidden;
            FiveThreeButton.Visibility = Visibility.Hidden;
            FiveFourButton.Visibility = Visibility.Hidden;
            FiveFiveButton.Visibility = Visibility.Hidden;
        }

        // Domino selection button method
        // It sets the first domino as their 'selected' domino and puts it into their selected place and then calls the ShowOptions method to show their placement options
        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Choose1))
            {
                player1Board.chosen = domino5;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.chosen.DominoBack, UriKind.Relative));
            }
            if(sender.Equals(Choose2))
            {
                player1Board.chosen = domino6;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.chosen.DominoBack, UriKind.Relative));
            }
            if (sender.Equals(Choose3))
            {
                player1Board.chosen = domino7;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.chosen.DominoBack, UriKind.Relative));
            }
            if (sender.Equals(Choose4))
            {
                player1Board.chosen = domino8;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.chosen.DominoBack, UriKind.Relative));
            }
            ShowTileSelectionButtons();
        }

        // Hides the domino selection buttons
        private void HideSelectDominoButtons()
        {
            Choose1.Visibility = Visibility.Hidden;
            Choose2.Visibility = Visibility.Hidden;
            Choose3.Visibility = Visibility.Hidden;
            Choose4.Visibility = Visibility.Hidden;
        }

        // Shows the domino selection buttons
        private void ShowSelectDominoButtons()
        {
            Choose1.Visibility = Visibility.Visible;
            Choose2.Visibility = Visibility.Visible;
            Choose3.Visibility = Visibility.Visible;
            Choose4.Visibility = Visibility.Visible;
        }

        // Calculate score method
        private void CalculateScores()
        {
            player1Board.CalculateScore();
            player2Board.CalculateScore();
            player3Board.CalculateScore();
            player4Board.CalculateScore();
        }

        private void Board_Click(object sender, RoutedEventArgs e)
        {
            if(sender.Equals(YourBoard))
            {
                RefreshBoard(player1Board);
            }
            if (sender.Equals(Player2Board))
            {
                RefreshBoard(player2Board);
            }
            if (sender.Equals(Player3Board))
            {
                RefreshBoard(player3Board);
            }
            if (sender.Equals(Player4Board))
            {
                RefreshBoard(player4Board);
            }
        }

        private void HideBoardButtons()
        {
            YourBoard.Visibility = Visibility.Hidden;
            Player2Board.Visibility = Visibility.Hidden;
            Player3Board.Visibility = Visibility.Hidden;
            Player4Board.Visibility = Visibility.Hidden;
        }

        private void ShowBoardButtons()
        {
            YourBoard.Visibility = Visibility.Visible;
            Player2Board.Visibility = Visibility.Visible;
            Player3Board.Visibility = Visibility.Visible;
            Player4Board.Visibility = Visibility.Visible;
        }

        private void ChatSend_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SelectTileButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender.Equals(SelectTile1Button))
            {
                ShowOptions();
                HideTileSelectionButtons();
            }
            if(sender.Equals(SelectTile2Button))
            {
                Tile tile1 = player1Board.chosen.Tile2;
                Tile tile2 = player1Board.chosen.Tile1;
                player1Board.chosen.Tile1 = tile1;
                player1Board.chosen.Tile2 = tile2;
                ShowOptions();
                HideTileSelectionButtons();
            }
        }

        private void ShowTileSelectionButtons()
        {
            SelectTile1Button.Visibility = Visibility.Visible;
            SelectTile2Button.Visibility = Visibility.Visible;
        }

        private void HideTileSelectionButtons()
        {
            SelectTile1Button.Visibility = Visibility.Hidden;
            SelectTile2Button.Visibility = Visibility.Hidden;
        }
    }
}
