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
        private readonly ViewModel viewModel;
        private Image[,] images;

        public Game()
        {
            viewModel = new ViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
            images = new Image[5, 5];
            CreateImageList();
        }

        // Sets the image where the button where the user clicked as one of their tiles of their selected domino.
        // It will set the first tile if it is their first pick and their second tile if it is their second pick.
        /*private void ChooseSpot_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType().IsVisible) //checks if object is visible, meaning it is a viable option
            {
                if (pick == 0)
                {
                    string tempString = sender.ToString();
                    String[] splitter = tempString.Split('_');


                    lastIpos = viewModel.ConvertNameFromStringToInt(splitter[0]);
                    lastJpos = viewModel.ConvertNameFromStringToInt(splitter[1]);

                    OnPropertyChanged("CurrentBoard[" + lastIpos + "][" + lastJpos + "]");

                    pick++;
                    HideOptions();
                    viewModel.ShowOptions();
                }
                else if (pick == 1)
                {
                    string tempString = sender.ToString();
                    String[] splitter = tempString.Split('_');


                    lastIpos = viewModel.ConvertNameFromStringToInt(splitter[0]);
                    lastJpos = viewModel.ConvertNameFromStringToInt(splitter[1]);

                    OnPropertyChanged("CurrentBoard[" + lastIpos + "][" + lastJpos + "]");
                    
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

                    HideMeeples();
                    viewModel.UpdateScores();
                    //Score.Text = "Score: " + player1Score.ToString();
                }
            }
        }

        //Shows the options where to place their tile
        
        private void HideOptions()
        {
            //Hide 

        }

        // Domino selection button method
        // It sets the first domino as their 'selected' domino and puts it into their selected place and then calls the ShowOptions method to show their placement options
        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Choose1))
            {
                player1Board.Chosen = domino5;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.Chosen.DominoBack, UriKind.Relative));

                MeepleChoice1.Visibility = Visibility.Visible;
            }
            if (sender.Equals(Choose2))
            {
                player1Board.Chosen = domino6;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.Chosen.DominoBack, UriKind.Relative));

                MeepleChoice2.Visibility = Visibility.Visible;
            }
            if (sender.Equals(Choose3))
            {
                player1Board.Chosen = domino7;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.Chosen.DominoBack, UriKind.Relative));

                MeepleChoice3.Visibility = Visibility.Visible;
            }
            if (sender.Equals(Choose4))
            {
                player1Board.Chosen = domino8;
                HideSelectDominoButtons();
                SelectedTile1.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile1.TileImage, UriKind.Relative));
                SelectedTile2.Source = new BitmapImage(new Uri(player1Board.Chosen.Tile2.TileImage, UriKind.Relative));
                SelectedDomino.Source = new BitmapImage(new Uri(player1Board.Chosen.DominoBack, UriKind.Relative));

                MeepleChoice4.Visibility = Visibility.Visible;
            }
            ShowTileSelectionButtons();
        }

        //Clears Meeples from board
        private void HideMeeples()
        {
            MeepleChoice1.Visibility = Visibility.Hidden;
            MeepleChoice2.Visibility = Visibility.Hidden;
            MeepleChoice3.Visibility = Visibility.Hidden;
            MeepleChoice4.Visibility = Visibility.Hidden;
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

        private void Board_Click(object sender, RoutedEventArgs e)
        {
            string tempString = ((Button)sender).GetValue(FrameworkElement.NameProperty) as string;
            int tempInt = Int32.Parse(tempString.Substring(2));
            viewModel.SwitchBoardView(tempInt);
        }

        private void HideBoardButtons()
        {
            Player.Board.Visibility = Visibility.Hidden;
            Player2Board.Visibility = Visibility.Hidden;
            Player3Board.Visibility = Visibility.Hidden;
            Player4Board.Visibility = Visibility.Hidden;
        }

        private void ShowBoardButtons()
        {
            //YourBoard.Visibility = Visibility.Visible;
            Player2Board.Visibility = Visibility.Visible;
            Player3Board.Visibility = Visibility.Visible;
            Player4Board.Visibility = Visibility.Visible;
        }

        private void SelectTileButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(SelectTile1Button))
            {
                ShowOptions();
                HideTileSelectionButtons();
            }
            if (sender.Equals(SelectTile2Button))
            {
                Tile tile1 = player1Board.Chosen.Tile2;
                Tile tile2 = player1Board.Chosen.Tile1;
                player1Board.Chosen.Tile1 = tile1;
                player1Board.Chosen.Tile2 = tile2;
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
        }*/

        private void Spot_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (((Button)sender).Name.Substring(0, ((Button)sender).Name.Length - 6).Equals(images[i,j].Name))
                    {
                        viewModel.UpdatePlacedTile(i, j);
                    }
                }
            }
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateChosenTile(Int32.Parse(((Button) sender).Name.Substring(12,1)));
        }

        private void Board_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SwitchBoardView(Int32.Parse(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1)) - 1);
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateChosenDomino(Int32.Parse(sender.ToString().Substring(sender.ToString().Length - 1)) - 1);
        }
        private void CreateImageList()
        {
            images[0, 0] = One_One;
            images[0, 1] = One_Two;
            images[0, 2] = One_Three;
            images[0, 3] = One_Four;
            images[0, 4] = One_Five;
            images[1, 0] = Two_One;
            images[1, 1] = Two_Two;
            images[1, 2] = Two_Three;
            images[1, 3] = Two_Four;
            images[1, 4] = Two_Five;
            images[2, 0] = Three_One;
            images[2, 1] = Three_Two;
            images[2, 2] = Three_Three;
            images[2, 3] = Three_Four;
            images[2, 4] = Three_Five;
            images[3, 0] = Four_One;
            images[3, 1] = Four_Two;
            images[3, 2] = Four_Three;
            images[3, 3] = Four_Four;
            images[3, 4] = Four_Five;
            images[4, 0] = Five_One;
            images[4, 1] = Five_Two;
            images[4, 2] = Five_Three;
            images[4, 3] = Five_Four;
            images[4, 4] = Five_Five;
        }
    }
}
