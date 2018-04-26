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
    public class ViewModel : INotifyPropertyChanged//, INotifyCollectionChanged
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
        public Visibility ShowChosenButtons { get; set; }
        public Visibility[][] BoardVisibility { get; set; }
        public Boolean[][] BoardEnable { get; set; }

        public string Score { get; set; }

        private int roundNumber = 1;
        private int pick = 1;

        private DominoHolder dominoHolder = new DominoHolder();

        public ViewModel()
        {
            PlayerList = new ObservableCollection<Player>();
            NextDominos = new ObservableCollection<Domino>();
            CurrentDominos = new ObservableCollection<Domino>();
            placeholderTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);
            InitializeBoardVisibilty();
            ShowButtons = Visibility.Visible;
            ShowChosenButtons = Visibility.Hidden;
            InitializeBoardEnable();
            CreatePlayers();
            CurrentBoard = PlayerList[0].Board;
            UpdateScores();
            SetBoardTileVisiblity();
            CreateBackFacingDominos();
            RotateDominoSelection();
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
        public void UpdateChosenDomino(int index)
        {
            ChosenDomino = CurrentDominos[index];
            OnPropertyChanged("ChosenDomino");
            ShowChosenButtons = Visibility.Visible;
            OnPropertyChanged("ShowChosenButtons");
            ShowButtons = Visibility.Hidden;
            OnPropertyChanged("ShowButtons");
        }
        public void UpdateChosenTile(int index)
        {
            if (index == 1)
            {
                ChosenTile = ChosenDomino.Tile1;
            }
            else
            {
                ChosenTile = ChosenDomino.Tile2;
            }
            ShowOptions(ChosenTile);
        }
        public void UpdatePlacedTile(int x, int y)
        {
            if (pick == 1)
            {
                CurrentBoard.Add(ChosenTile, x, y);
                OnPropertyChanged("CurrentBoard");
                CheckNextOptions(x, y, ChosenTile);
                ShowChosenButtons = Visibility.Hidden;
                OnPropertyChanged("ShowChosenButtons");
                pick++;
            }
            else
            {
                if (ChosenTile.Equals(ChosenDomino.Tile1))
                {
                    ChosenTile = ChosenDomino.Tile2;
                }
                else
                {
                    ChosenTile = ChosenDomino.Tile1;
                }
                CurrentBoard.Add(ChosenTile, x, y);
                OnPropertyChanged("CurrentBoard");
                NullifyPlaceHolder();
                SetBoardTileVisiblity();
                UpdateScores();
                pick = 1;
                RotateDominoSelection();
                /** 
                 * 
                 * TODO
                 * 
                 * **/
                ShowButtons = Visibility.Visible;
                OnPropertyChanged("ShowButtons");
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
            CurrentBoard = PlayerList[index].Board;
            OnPropertyChanged("CurrentBoard");
            UpdateScores();
            SetBoardTileVisiblity();
        }

        private void NullifyPlaceHolder()
        {
            for(int row = 0; row < 5; row ++)
            {
                for(int col = 0; col < 5; col++)
                {
                    if(CurrentBoard.PlayBoard[row][col] != null && CurrentBoard.PlayBoard[row][col] == placeholderTile)
                    {
                        CurrentBoard.PlayBoard[row][col] = null;
                    }
                }
            }
        }
        private void EnablePlaceholderButtons()
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (CurrentBoard.PlayBoard[row][col] != null && CurrentBoard.PlayBoard[row][col] == placeholderTile)
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
            OnPropertyChanged("BoardEnable");
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
        public void UpdateScores()
        {
            Score = "Score: " + CurrentBoard.CalculateScore();
            OnPropertyChanged("Score");
        }
        public void SetCurrentDominosFromNextDominos()
        {
            CurrentDominos.Clear();
            foreach (Domino domino in NextDominos)
            {
                CurrentDominos.Add(domino);
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
        public void ShowOptions(Tile chosenTile)
        {
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
                            CheckAvailableMoves(row, col, chosenTile);
                        }
                    }
                }
            }
            OnPropertyChanged("CurrentBoard");
            SetBoardTileVisiblity();
            EnablePlaceholderButtons();
        }
        private void CheckNextOptions(int row, int col, Tile tile)
        {
            NullifyPlaceHolder();
            CheckAvailableMoves(row, col, tile);
            OnPropertyChanged("CurrentBoard");
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
            if (CurrentBoard.PlayBoard[row][col] == null)
            {
                CurrentBoard.PlayBoard[row][col] = placeholderTile;
            }
        }
        private void InitializeBoardVisibilty()
        {
            int ROW = 5;

            BoardVisibility = new Visibility[ROW][];

            for (int col = 0; col < 5; col++)
            {
                BoardVisibility[col] = new Visibility[ROW];
            }
        }
        private void InitializeBoardEnable()
        {
            int ROWS = 5;
            BoardEnable = new Boolean[ROWS][];

            for (int col = 0; col < 5; col++)
            {
                BoardEnable[col] = new Boolean[ROWS];
            }
        }
    }
}