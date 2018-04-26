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
    class ViewModel : INotifyPropertyChanged
    {
        private readonly Tile placeholderTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
        private string chatHistory;
        public string ChatHistory
        {
            get { return chatHistory; }
            set { chatHistory = value; }
        }
        public ObservableCollection<Player> PlayerList { get; set; }
        public ObservableCollection<Domino> NextDominos { get; set; }
        public ObservableCollection<Domino> CurrentDominos { get; set; }
        public Player CurrentPlayer { get; set; }
        public Tile ChosenTile { get; set; }

        public Visibility ShowButtons { get; set; }
        public Visibility ShowChosenButtons { get; set; }
        public Visibility[][] BoardVisibility { get; set; }
        public Boolean[][] BoardEnable { get; set; }

        public Visibility[] Choose { get; set; }
        private Boolean[] Chosen { get; set; }

        public string Score { get; set; }

        public int roundNumber = 1;
        public int pick = 1;
        public int turn = 1;

        private DominoHolder dominoHolder = new DominoHolder();

        public ViewModel()
        {
            PlayerList = new ObservableCollection<Player>();
            NextDominos = new ObservableCollection<Domino>();
            CurrentDominos = new ObservableCollection<Domino>();

            InitVisiblityAndEnability();
            Chosen = new Boolean[4];
            CreatePlayers();

            CurrentPlayer = PlayerList[0];

            UpdateScores();

            SetBoardTileVisiblity();

            CreateBackFacingDominos();
            SetCurrentDominosFromNextDominos();
            CreateBackFacingDominos();
        }

        private void InitVisiblityAndEnability()
        {
            BoardVisibility = new Visibility[5][];
            BoardVisibility[0] = new Visibility[5];
            BoardVisibility[1] = new Visibility[5];
            BoardVisibility[2] = new Visibility[5];
            BoardVisibility[3] = new Visibility[5];
            BoardVisibility[4] = new Visibility[5];

            ShowButtons = Visibility.Visible;
            ShowChosenButtons = Visibility.Hidden;

            BoardEnable = new Boolean[5][];
            BoardEnable[0] = new Boolean[5];
            BoardEnable[1] = new Boolean[5];
            BoardEnable[2] = new Boolean[5];
            BoardEnable[3] = new Boolean[5];
            BoardEnable[4] = new Boolean[5];

            Choose = new Visibility[4];
            Choose[0] = Visibility.Visible;
            Choose[1] = Visibility.Visible;
            Choose[2] = Visibility.Visible;
            Choose[3] = Visibility.Visible;
        }

        private void CreatePlayers()
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerList.Add(new Player("Player " + (i + 1)));
                PlayerList[i].Turn = i + 1;
            }
        }
        private void DisplayChatMessage(int index, string text)
        {
            ChatHistory += PlayerList[index].Name + ": " + text + "\n";
            OnPropertyChanged("ChatHistory");
        }
        public void UpdateChosenDomino(int index)
        {
            SwitchToCorrectBoard();
            CurrentPlayer.Board.Chosen = CurrentDominos[index];
            OnPropertyChanged("CurrentPlayer");
            Chosen[index] = true;
            ShowChosenButtons = Visibility.Visible;
            OnPropertyChanged("ShowChosenButtons");
            for(int i = 0; i < 4; i++)
            {
                Choose[i] = Visibility.Hidden;
            }
            OnPropertyChanged("Choose");
            ShowButtons = Visibility.Hidden;
            OnPropertyChanged("ShowButtons");
        }
        public void UpdateChosenTile(int index)
        {
            if (index == 1)
            {
                ChosenTile = CurrentPlayer.Board.Chosen.Tile1;
            }
            else
            {
                ChosenTile = CurrentPlayer.Board.Chosen.Tile2;
            }
            ShowOptions(ChosenTile);
        }

        public void UpdatePlacedTile(int x, int y)
        {
            if (pick == 1)
            {
                CurrentPlayer.Board.Add(ChosenTile, x, y);
                OnPropertyChanged("CurrentPlayer");
                CheckNextOptions(x, y, ChosenTile);
                ShowChosenButtons = Visibility.Hidden;
                OnPropertyChanged("ShowChosenButtons");
                pick++;
            }
            else
            {
                if (ChosenTile.Equals(CurrentPlayer.Board.Chosen.Tile1))
                {
                    ChosenTile = CurrentPlayer.Board.Chosen.Tile2;
                }
                else
                {
                    ChosenTile = CurrentPlayer.Board.Chosen.Tile1;
                }
                CurrentPlayer.Board.Add(ChosenTile, x, y);
                OnPropertyChanged("CurrentPlayer");
                NullifyPlaceHolder();
                EnablePlaceholderButtons();
                SetBoardTileVisiblity();
                UpdateScores();
                pick = 1;
                if (turn == 4)
                {
                    turn = 1;
                    RotateDominoSelection();
                    SetPlayerTurns();
                    for (int i = 0; i < 4; i++)
                    {
                        Chosen[i] = false;
                    }
                    OnPropertyChanged("Choose");
                }
                else
                {
                    turn++;
                }
                /** 
                 * 
                 * TODO
                 * 
                 * **/

                SwitchToCorrectBoard();
                DisplayUnchosenDominos();
                OnPropertyChanged("Choose");
                ShowButtons = Visibility.Visible;
                OnPropertyChanged("ShowButtons");
            }
        }

        private void DisplayUnchosenDominos()
        {
            for(int i = 0; i < 4; i++)
            {
                if(Chosen[i] == false)
                {
                    Choose[i] = Visibility.Visible;
                }
            }
        }

        private void SetPlayerTurns()
        {
            // 0 first
            if(PlayerList[0].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number)
            {
                PlayerList[0].Turn = 1;
                PlayerList[1].Turn = 2;
                PlayerList[2].Turn = 3;
                PlayerList[3].Turn = 4;
            }
            else if (PlayerList[0].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number)
            {
                PlayerList[0].Turn = 1;
                PlayerList[1].Turn = 2;
                PlayerList[3].Turn = 3;
                PlayerList[2].Turn = 4;
            }
            else if (PlayerList[0].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number)
            {
                PlayerList[0].Turn = 1;
                PlayerList[3].Turn = 2;
                PlayerList[1].Turn = 3;
                PlayerList[2].Turn = 4;
            }
            else if (PlayerList[0].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number)
            {
                PlayerList[0].Turn = 1;
                PlayerList[2].Turn = 2;
                PlayerList[1].Turn = 3;
                PlayerList[3].Turn = 4;
            }
            else if (PlayerList[0].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number)
            {
                PlayerList[0].Turn = 1;
                PlayerList[2].Turn = 2;
                PlayerList[3].Turn = 3;
                PlayerList[1].Turn = 4;
            }
            else if (PlayerList[0].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number)
            {
                PlayerList[0].Turn = 1;
                PlayerList[3].Turn = 2;
                PlayerList[2].Turn = 3;
                PlayerList[1].Turn = 4;
            }

            // 1 first
            else if (PlayerList[1].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number)
            {
                PlayerList[1].Turn = 1;
                PlayerList[0].Turn = 2;
                PlayerList[2].Turn = 3;
                PlayerList[3].Turn = 4;
            }
            else if (PlayerList[1].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number)
            {
                PlayerList[1].Turn = 1;
                PlayerList[2].Turn = 2;
                PlayerList[0].Turn = 3;
                PlayerList[3].Turn = 4;
            }
            else if (PlayerList[1].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number)
            {
                PlayerList[1].Turn = 1;
                PlayerList[0].Turn = 2;
                PlayerList[3].Turn = 3;
                PlayerList[2].Turn = 4;
            }
            else if (PlayerList[1].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number)
            {
                PlayerList[1].Turn = 1;
                PlayerList[3].Turn = 2;
                PlayerList[0].Turn = 3;
                PlayerList[2].Turn = 4;
            }
            else if (PlayerList[1].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number)
            {
                PlayerList[1].Turn = 1;
                PlayerList[3].Turn = 2;
                PlayerList[2].Turn = 3;
                PlayerList[0].Turn = 4;
            }
            else if (PlayerList[1].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number)
            {
                PlayerList[1].Turn = 1;
                PlayerList[2].Turn = 2;
                PlayerList[3].Turn = 3;
                PlayerList[0].Turn = 4;
            }

            // 2 first
            else if (PlayerList[2].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number)
            {
                PlayerList[2].Turn = 1;
                PlayerList[1].Turn = 2;
                PlayerList[0].Turn = 3;
                PlayerList[3].Turn = 4;
            }
            else if (PlayerList[2].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number)
            {
                PlayerList[2].Turn = 1;
                PlayerList[0].Turn = 2;
                PlayerList[1].Turn = 3;
                PlayerList[3].Turn = 4;
            }
            else if (PlayerList[2].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number)
            {
                PlayerList[2].Turn = 1;
                PlayerList[1].Turn = 2;
                PlayerList[3].Turn = 3;
                PlayerList[0].Turn = 4;
            }
            else if (PlayerList[2].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number)
            {
                PlayerList[2].Turn = 1;
                PlayerList[3].Turn = 2;
                PlayerList[1].Turn = 3;
                PlayerList[0].Turn = 4;
            }
            else if (PlayerList[2].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number)
            {
                PlayerList[2].Turn = 1;
                PlayerList[0].Turn = 2;
                PlayerList[3].Turn = 3;
                PlayerList[1].Turn = 4;
            }
            else if (PlayerList[2].Board.Chosen.Number < PlayerList[3].Board.Chosen.Number && PlayerList[3].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number)
            {
                PlayerList[2].Turn = 1;
                PlayerList[3].Turn = 2;
                PlayerList[0].Turn = 3;
                PlayerList[1].Turn = 4;
            }

            // 3 first
            else if (PlayerList[3].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number)
            {
                PlayerList[3].Turn = 1;
                PlayerList[1].Turn = 2;
                PlayerList[2].Turn = 3;
                PlayerList[0].Turn = 4;
            }
            else if (PlayerList[3].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number)
            {
                PlayerList[3].Turn = 1;
                PlayerList[2].Turn = 2;
                PlayerList[1].Turn = 3;
                PlayerList[0].Turn = 4;
            }
            else if (PlayerList[3].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number)
            {
                PlayerList[3].Turn = 1;
                PlayerList[1].Turn = 2;
                PlayerList[0].Turn = 3;
                PlayerList[2].Turn = 4;
            }
            else if (PlayerList[3].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number && PlayerList[1].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number)
            {
                PlayerList[3].Turn = 1;
                PlayerList[0].Turn = 2;
                PlayerList[1].Turn = 3;
                PlayerList[2].Turn = 4;
            }
            else if (PlayerList[3].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number)
            {
                PlayerList[3].Turn = 1;
                PlayerList[0].Turn = 2;
                PlayerList[2].Turn = 3;
                PlayerList[1].Turn = 4;
            }
            else if (PlayerList[3].Board.Chosen.Number < PlayerList[2].Board.Chosen.Number && PlayerList[2].Board.Chosen.Number < PlayerList[0].Board.Chosen.Number && PlayerList[0].Board.Chosen.Number < PlayerList[1].Board.Chosen.Number)
            {
                PlayerList[3].Turn = 1;
                PlayerList[2].Turn = 2;
                PlayerList[0].Turn = 3;
                PlayerList[1].Turn = 4;
            }
            else
            {
                Console.WriteLine("ERROR PLAYER TURN");
            }
            OnPropertyChanged("PlayerList");
        }

        private void SwitchToCorrectBoard()
        {
            
            if (turn == 1)
            {
                for(int i = 0; i < 4; i++)
                {
                    if(PlayerList[i].Turn == 1)
                    {
                        SwitchBoardView(i);
                    }
                }
            }
            else if (turn == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerList[i].Turn == 2)
                    {
                        SwitchBoardView(i);
                    }
                }
            }
            else if (turn == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerList[i].Turn == 3)
                    {
                        SwitchBoardView(i);
                    }
                }
            }
            else if (turn == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerList[i].Turn == 4)
                    {
                        SwitchBoardView(i);
                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR DISPLAY CORRECT BOARD");
            }
        }

        private void RotateDominoSelection()
        {
            if (roundNumber <= 10)
            {
                SetCurrentDominosFromNextDominos();
                CreateBackFacingDominos();
            }
            else if (roundNumber == 11)
            {
                SetCurrentDominosFromNextDominos();
                NextDominos.Clear();
            }
            roundNumber++;
        }

        public void SwitchBoardView(int index)
        {
            CurrentPlayer = PlayerList[index];
            OnPropertyChanged("CurrentPlayer");
            UpdateScores();
            SetBoardTileVisiblity();
        }

        private void NullifyPlaceHolder()
        {
            for(int row = 0; row < 5; row ++)
            {
                for(int col = 0; col < 5; col++)
                {
                    if(CurrentPlayer.Board.PlayBoard[row][col] != null && CurrentPlayer.Board.PlayBoard[row][col] == placeholderTile)
                    {
                        CurrentPlayer.Board.PlayBoard[row][col] = null;
                    }
                }
            }
            OnPropertyChanged("CurrentPlayer");
        }

        private void EnablePlaceholderButtons()
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (CurrentPlayer.Board.PlayBoard[row][col] != null && CurrentPlayer.Board.PlayBoard[row][col] == placeholderTile)
                    {
                        BoardEnable[row][col] = true;  
                    }
                    else
                    {
                        BoardEnable[row][col] = false;
                    }
                }
            }
            OnPropertyChanged("BoardEnable");
        }
        private void SetBoardTileVisiblity()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(CurrentPlayer.Board.PlayBoard[i][j] != null)
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateScores()
        {
            Score = "" + CurrentPlayer.Board.CalculateScore();
            OnPropertyChanged("Score");
        }

        private void SetCurrentDominosFromNextDominos()
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

        private void CreateBackFacingDominos()
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
                    {
                        Exchange(NextDominos, i, i + 1);
                    }
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

        private void ShowOptions(Tile chosenTile)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (CurrentPlayer.Board.PlayBoard[row][col] != null)
                    {
                        Tile tempTile = CurrentPlayer.Board.PlayBoard[row][col];
                        TileType tempTileType = tempTile.TileType;

                        if (chosenTile.TileType == tempTileType || tempTileType == TileType.Origin)
                        {
                            CheckAvailableMoves(row, col, chosenTile);
                        }
                    }
                }
            }
            OnPropertyChanged("CurrentPlayer");
            SetBoardTileVisiblity();
            EnablePlaceholderButtons();
        }

        private void CheckNextOptions(int row, int col, Tile tile)
        {
            NullifyPlaceHolder();
            CheckAvailableMoves(row, col, tile);
            OnPropertyChanged("CurrentPlayer");
            SetBoardTileVisiblity();
            EnablePlaceholderButtons();
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
            if (CurrentPlayer.Board.PlayBoard[row][col] == null)
            {
                CurrentPlayer.Board.PlayBoard[row][col] = placeholderTile;
            }
        }

        public String CalculateWinner()
        {
            String display = "";
            if (PlayerList[0].Board.CalculateScore() > PlayerList[1].Board.CalculateScore() && PlayerList[0].Board.CalculateScore() > PlayerList[2].Board.CalculateScore() && PlayerList[0].Board.CalculateScore() > PlayerList[3].Board.CalculateScore())
            {
                display = "Winner: " + PlayerList[0].Name;
            }
            else if (PlayerList[1].Board.CalculateScore() > PlayerList[0].Board.CalculateScore() && PlayerList[1].Board.CalculateScore() > PlayerList[2].Board.CalculateScore() && PlayerList[1].Board.CalculateScore() > PlayerList[3].Board.CalculateScore())
            {
                display = "Winner: " + PlayerList[1].Name;
            }
            else if (PlayerList[2].Board.CalculateScore() > PlayerList[0].Board.CalculateScore() && PlayerList[2].Board.CalculateScore() > PlayerList[1].Board.CalculateScore() && PlayerList[2].Board.CalculateScore() > PlayerList[3].Board.CalculateScore())
            {
                display = "Winner: " + PlayerList[2].Name;
            }
            else if (PlayerList[3].Board.CalculateScore() > PlayerList[0].Board.CalculateScore() && PlayerList[3].Board.CalculateScore() > PlayerList[1].Board.CalculateScore() && PlayerList[3].Board.CalculateScore() > PlayerList[2].Board.CalculateScore())
            {
                display = "Winner: " + PlayerList[3].Name;
            }
            else
            {
                display = "Tie";
            }
            for (int i = 0; i < 4; i++)
            {
                display += "\n" + PlayerList[i].Name + ": " + PlayerList[i].Board.CalculateScore();
            }
            return display;
        }

        private void CheckValidMove()
        {

        }
    }
}