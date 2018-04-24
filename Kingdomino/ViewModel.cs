using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace KingDomino
{
    class ViewModel : INotifyPropertyChanged//, INotifyCollectionChanged
    {
        private Tile placeholderTile;
        private string chatHistory;
        public string ChatHistory
        {
            get { return chatHistory; }
            set { chatHistory = value; }
        }
        public ObservableCollection<Player> PlayerList { get; set; }
        public ObservableCollection<Domino> NextDominos { get; set; }
        public ObservableCollection<Domino> CurrentDominos { get; set; }
        public Board CurrentBoard { get; set; }
        public Domino ChosenDomino { get; set; }
        public Tile ChosenTile { get; set; }

        public Visibility ShowButtons { get; set; }
        public Visibility[][] BoardVisibility { get; set; }

        public string Score { get; set; }

        private int roundNumber = 1;

        private DominoHolder dominoHolder = new DominoHolder();

        public ViewModel()
        {
            placeholderTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);

            PlayerList = new ObservableCollection<Player>();
            NextDominos = new ObservableCollection<Domino>();
            CurrentDominos = new ObservableCollection<Domino>();
            
            BoardVisibility = new Visibility[5][];
            BoardVisibility[0] = new Visibility[5];
            BoardVisibility[1] = new Visibility[5];
            BoardVisibility[2] = new Visibility[5];
            BoardVisibility[3] = new Visibility[5];
            BoardVisibility[4] = new Visibility[5];
            CreatePlayers();
            CurrentBoard = PlayerList[0].Board;
            ShowButtons = Visibility.Visible;
            SetBoardTileVisiblity();
            CreateBackFacingDominos();
            SetCurrentDominosFromNextDominos();
            CreateBackFacingDominos();
        }

        public void CreatePlayers()
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerList.Add(new Player());
            }
        }

        public void DisplayChatMessage(int index, string text)
        {
            ChatHistory += PlayerList[index].Name + ": " + text + "\n";
            OnPropertyChanged("ChatHistory");
        }

        public void UpdatePlacedTile(int x, int y)
        {
            CurrentBoard.Add(ChosenTile, x, y);
            NullifyPlaceHolder();
            OnPropertyChanged("CurrentBoard");
        }

        public void SwitchBoardView(int index)
        {
            CurrentBoard = PlayerList[index].Board;
            OnPropertyChanged("CurrentBoard");
        }

        public void SetBoardTileVisiblity()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(CurrentBoard.PlayBoard[i][j] != null)
                    {
                        BoardVisibility[i][j] = Visibility.Visible;
                    }
                    else
                    {
                        BoardVisibility[i][j] = Visibility.Hidden;
                    }
                }
            }
            OnPropertyChanged("BoardVisibility");
        }

        // INotifyPropertyChanged: 
        // OnPropertyChanged must be called to tell a view bound to this implementation to get specified updated property
        public event PropertyChangedEventHandler PropertyChanged;
        // INotifyCollectionChanged:
        // May be what we need instead of propertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateChosenDomino(int index)
        {
            ChosenDomino = CurrentDominos[index];
            OnPropertyChanged("ChosenDomino");
            if(roundNumber <= 10)
            {
                SetCurrentDominosFromNextDominos();
                CreateBackFacingDominos();
            }
            else if(roundNumber == 11)
            {
                SetCurrentDominosFromNextDominos();
                NextDominos.Clear();
            }
            ShowButtons = Visibility.Hidden;
            OnPropertyChanged("ShowButtons");
            roundNumber++;
        }

        public void UpdateChosenTile(int index)
        {
            if(index == 1)
            {
                ChosenTile = ChosenDomino.Tile1;
            }
            else
            {
                ChosenTile = ChosenDomino.Tile2;
            }
            ShowOptions(ChosenTile);
        }

        public void UpdateScores()
        {
            foreach (Player player in PlayerList)
            {
                player.Board.CalculateScore();
            }

            OnPropertyChanged("Score");
        }

        public void SetCurrentDominosFromNextDominos()
        {
            int index = 0;
            CurrentDominos.Clear();
            foreach (Domino domino in NextDominos)
            {
                CurrentDominos.Add(domino);
                ++index;
            }
            OnPropertyChanged("CurrentDominos");
        }
        private void GetFourRandomDominos()
        {
            NextDominos.Clear();
            for (int i = 0; i < 4; i++)
            {
                NextDominos.Add(dominoHolder.RandomDomino());
            }
        }
        public void CreateBackFacingDominos()
        {
            GetFourRandomDominos();

            SortDominos();
            
            OnPropertyChanged("NextDominos");

        }

        private void SortDominos()
        {
            int size = NextDominos.Count;
            for (int j = size - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    if (NextDominos[i].Number > NextDominos[i + 1].Number)
                        Exchange(NextDominos, i, i + 1);
                }
            }
        }

        private void Exchange(ObservableCollection<Domino> dominos, int m, int n)
        {
            Domino tempDomino;

            tempDomino = dominos[m];
            dominos[m] = dominos[n];
            dominos[n] = tempDomino;
        }

        private void NullifyPlaceHolder()
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (CurrentBoard.PlayBoard[row][col] != null && CurrentBoard.PlayBoard[row][col] == placeholderTile)
                    {
                        CurrentBoard.PlayBoard[row][col] = null;
                    }
                }
            }
        }

        public void ShowOptions(Tile chosenTile)
        {
            NullifyPlaceHolder();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (CurrentBoard.PlayBoard[row][col] != null)
                    {
                        Tile tempTile = CurrentBoard.PlayBoard[row][col];
                        TileType tempTileType = tempTile.TileType;

                        if (chosenTile.TileType == tempTileType || tempTileType == TileType.Origin)
                        {
                            //check directions here
                            CheckAvailableMoves(row, col, chosenTile);                          
                        }
                    }
                }
            }
            OnPropertyChanged("CurrentBoard");
            SetBoardTileVisiblity();
        }
        private void CheckAvailableMoves(int row, int col, Tile tile)
        {
            Boolean north = row > 0;
            Boolean west = col > 0;
            Boolean south = row < 4;
            Boolean east = col < 4;

            if (north)
            {
                CheckDirection(row - 1, col, tile);
            }

            if (west)
            {
                CheckDirection(row, col - 1, tile);
            }

            if (south)
            {
                CheckDirection(row + 1, col, tile);
            }

            if (east)
            {
                CheckDirection(row, col + 1, tile);
            }
        }

        private void CheckDirection(int row, int col, Tile tile)
        {
            //Tile tempTile = CurrentBoard.PlayBoard[row][col];

            if (CurrentBoard.PlayBoard[row][col] == null)
            {
                CurrentBoard.PlayBoard[row][col] = placeholderTile;
            }
        }

        // Refreshs the four starting dominos and flips the next ones
        /*public void RefreshSelectionDominos()
        {

            //Create new backfacing dominos and set the old ones as the new tiles
            if (roundNumber <= 10)
            {
                SetCurrentDominosFromNextDominos();
                roundNumber++;
            }

            //Removes the backfacing dominos once there is no more dominos left in the DominoHolder and then sets the last dominos as the new tiles
            else if (roundNumber == 11)
            {
                SetCurrentDominosFromNextDominos();
                roundNumber++;

            }

            //Removes the tiles now that there is no more tiles or dominos
            else if (roundNumber == 13)
            {
                SetCurrentDominosFromNextDominos();
                roundNumber++;
            }
            else
            {

                //MainWindow main = new MainWindow();
                //main.Show();
                //Close();
            }
        }*/

        public int ConvertNameFromStringToInt(string imageName)
        {
            switch(imageName)
            {
                case ("One"):
                    return 1;
                case ("Two"):
                    return 2;
                case ("Three"):
                    return 3;
                case ("Four"):
                    return 4;
                case ("Five"):
                    return 5;

                default:
                    return -1;
            }
        }

        private void CheckMove()
        {

        }
        /*
        public void ShowOptions(int playerNumber)
        {
            HideOptions();
            RefreshBoard(player1Board);
            HideBoardButtons();

            Player tempPlayer = PlayerList[playerNumber];
            Board tempBoard = tempPlayer.Board;
            Tile[,] tempPlayBoard = tempPlayer.Board.PlayBoard;

            if (pick == 0)
            {
                // Up or Down button visibility
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        if (tempPlayBoard[i, j] != null)
                        {
                            if (tempPlayBoard[i, j + 1] == null && (tempPlayBoard[i,j].TileType == tempBoard.Chosen.Tile1.TileType) || (tempPlayBoard[i, j].TileType == TileType.Origin))
                            {
                                if (j + 1 > 1)
                                {
                                    images[i, j + 1].Visibility = Visibility.Visible;
                                }
                            }
                            
                            if (tempPlayBoard[i , j - 1] == null && (tempPlayBoard[i, j].TileType == tempBoard.Chosen.Tile1.TileType) || (tempPlayBoard[i, j].TileType == TileType.Origin))
                            {
                                if (j - 1 < 3)
                                {
                                    images[i, j - 1].Visibility = Visibility.Visible;
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
                            if (player1Board.PlayBoard[i + 1, j] == null && (player1Board.PlayBoard[i, j].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[i, j].TileType.Equals("Origin")))
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
                            if (player1Board.PlayBoard[i - 1, j] == null && (player1Board.PlayBoard[i, j].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[i, j].TileType.Equals("Origin")))
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
                for (int i = 0; i < 4; i++)
                {
                    if (player1Board.PlayBoard[4, i] != null)
                    {
                        if (player1Board.PlayBoard[4, i + 1] == null && (player1Board.PlayBoard[4, i].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[4, i].TileType.Equals("Origin")))
                        {
                            if (i == 0)
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
                        if (player1Board.PlayBoard[3, i] == null && (player1Board.PlayBoard[4, i].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[4, i].TileType.Equals("Origin")))
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
                        if (player1Board.PlayBoard[i + 1, 4] == null && (player1Board.PlayBoard[i, 4].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[i, 4].TileType.Equals("Origin")))
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
                        if (player1Board.PlayBoard[i, 3] == null && (player1Board.PlayBoard[i, 4].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[i, 4].TileType.Equals("Origin")))
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
                        if (player1Board.PlayBoard[4, i - 1] == null && (player1Board.PlayBoard[4, i].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[4, i].TileType.Equals("Origin")))
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
                        if (player1Board.PlayBoard[i - 1, 4] == null && (player1Board.PlayBoard[i, 4].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[i, 4].TileType.Equals("Origin")))
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
                    if (player1Board.PlayBoard[4, 3] == null && (player1Board.PlayBoard[4, 4].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[4, 4].TileType.Equals("Origin")))
                    {
                        FiveFourButton.Visibility = Visibility.Visible;
                    }
                    if (player1Board.PlayBoard[3, 4] == null && (player1Board.PlayBoard[4, 4].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[4, 4].TileType.Equals("Origin")))
                    {
                        FourFiveButton.Visibility = Visibility.Visible;
                    }
                }
                if (player1Board.PlayBoard[0, 0] != null)
                {
                    if (player1Board.PlayBoard[1, 0] == null && (player1Board.PlayBoard[0, 0].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[0, 0].TileType.Equals("Origin")))
                    {
                        TwoOneButton.Visibility = Visibility.Visible;
                    }
                    if (player1Board.PlayBoard[0, 1] == null && (player1Board.PlayBoard[0, 0].TileType.Equals(player1Board.Chosen.Tile1.TileType) || player1Board.PlayBoard[0, 0].TileType.Equals("Origin")))
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

            if (!(OneOneButton.IsVisible || OneTwoButton.IsVisible || OneThreeButton.IsVisible || OneFourButton.IsVisible || OneFiveButton.IsVisible ||
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
        }*/
    }
}
