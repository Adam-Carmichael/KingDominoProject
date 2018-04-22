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

        private int pick = 0;

        private int lastIpos = 0;       // Column position of last tile placed (board is at this point in time arranged by i columns and j rows)
        private int lastJpos = 0;       // Row position of last tile placed

        public Game()
        {
            viewModel = new ViewModel();
            DataContext = viewModel;
            InitializeComponent();
            viewModel.CreateBackFacingDominos();
            viewModel.SetCurrentDominosFromNextDominos();
            viewModel.CreateBackFacingDominos();
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
    
        private void BoardClick(object sender, RoutedEventArgs e)
        {
            viewModel.UpdatePlacedTile(1,1);
        }

        private void Board_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CreateImageList()
        {
            viewModel.AddImageToList(1, 1, One_One);
            viewModel.AddImageToList(1, 2, One_Two);
            viewModel.AddImageToList(1, 3, One_Three);
            viewModel.AddImageToList(1, 4, One_Four);
            viewModel.AddImageToList(1, 5, One_Five);
            viewModel.AddImageToList(2, 1, Two_One);
            viewModel.AddImageToList(2, 2, Two_Two);
            viewModel.AddImageToList(2, 3, Two_Three);
            viewModel.AddImageToList(2, 4, Two_Four);
            viewModel.AddImageToList(2, 5, Two_Five);
            viewModel.AddImageToList(3, 1, Three_One);
            viewModel.AddImageToList(3, 2, Three_Two);
            viewModel.AddImageToList(3, 3, Three_Three);
            viewModel.AddImageToList(3, 4, Three_Four);
            viewModel.AddImageToList(3, 5, Three_Five);
            viewModel.AddImageToList(4, 1, Four_One);
            viewModel.AddImageToList(4, 2, Four_Two);
            viewModel.AddImageToList(4, 3, Four_Three);
            viewModel.AddImageToList(4, 4, Four_Four);
            viewModel.AddImageToList(4, 5, Four_Five);
            viewModel.AddImageToList(5, 1, Five_One);
            viewModel.AddImageToList(5, 2, Five_Two);
            viewModel.AddImageToList(5, 3, Five_Three);
            viewModel.AddImageToList(5, 4, Five_Four);
            viewModel.AddImageToList(5, 5, Five_Five);
        }

    }
}