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
using PeerManager;
using DataModels;

namespace KingDomino
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IMessenger _msgHandler;
        private int thisPlayerID;
        private Tile placeholderTile;
        private bool host;
        private int roundNumber = 1;
        private int pick = 1;
        private DominoHolder dominoHolder = new DominoHolder();

        // Binding Properties for MainWindow
        public Visibility ButtonsVisible { get; set; }
        public Visibility NameInputVisible { get; set; }

        // Binding Properties for Game
        public string ChatHistory { get; set; }
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
        
        public ViewModel()
        {
            ButtonsVisible = Visibility.Visible;
            NameInputVisible = Visibility.Hidden;

            PlayerList = new ObservableCollection<Player>() {null, null, null, null};
            NextDominos = new ObservableCollection<Domino>();
            CurrentDominos = new ObservableCollection<Domino>();

            placeholderTile = new Tile("Resources/Misc/logo.png", TileType.Null, 0);

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
            

            CreateBackFacingDominos();
            SetCurrentDominosFromNextDominos();
            CreateBackFacingDominos();
        }

        public void CreatePlayers()
        {
            CurrentBoard = PlayerList[0].Board;

            UpdateScores();

            SetBoardTileVisiblity();
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

                if (host)
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
                        //BoardEnable[i][j] = true;
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

        public void UpdateScores()
        {
            Score = "Score: " + CurrentBoard.CalculateScore();
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

        private void GetFourSpecifiedDominos(int[] nums)
        {
            NextDominos.Clear();
            for (int i = 0; i < 4; i++)
            {
                NextDominos.Add(dominoHolder.SpecificDomino(nums[i]));
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

        /*
         *
         * INotifyPropertyChanged:
         * OnPropertyChanged must be called to tell a view bound to this implementation to get specified updated property
         *
         */
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*
         *
         *
         * Networking
         *
         */

        public void SendChat(string text)
        {
            _msgHandler.SendChatMessage(thisPlayerID, text);
        }

        public IMessenger InitComm(bool isHost)
        {
            this.host = isHost;
            this._msgHandler = new Messenger(host, ReceiveMessage);
            return _msgHandler;
        }

        // message delegate determines what to do with inbound information
        public void ReceiveMessage(SerializedMessage message)
        {
            switch (message.Purpose)
            {
                case Purpose.Query:
                    _msgHandler.SendPlayerUpdate(message.PeerId, PlayerList[message.PeerId]);
                    break;
                case Purpose.Init:
                    InitThisPlayer(message.PeerId, message.PlayerName);
                    break;
                case Purpose.Chat:
                    DisplayChatMessage(message.PeerId, message.Text);
                    break;
                case Purpose.Deal:
                    int[] nums = message.Dominos;
                    GetFourSpecifiedDominos(nums);
                    break;
                case Purpose.Select:
                    break;
                case Purpose.Tile:
                    UpdateOtherPlayerTile(message.PeerId, message.Domino, message.Side, message.Xcoord, message.Ycoord);
                    break;
                case Purpose.Player:
                    UpdatePlayerData(message.PeerId, message.IsFull, message.PlayerName);
                    break;
                default:
                    DisplayChatMessage(0, "Error: Network message not recognized");
                    break;
            }
        }

        // establishes the identity of this client
        public void InitThisPlayer(int playerNum, string name)
        {
            thisPlayerID = playerNum;
            UpdatePlayerData(playerNum, true, name);
            _msgHandler.SendPlayerUpdate(playerNum, PlayerList[playerNum]);
            ChatHistory += String.Format("Hi, {0}! You have joined as Player {1}\n", name, playerNum);
            OnPropertyChanged("ChatHistory");
        }

        public void UpdatePlayerData(int index, bool isFull, string name)
        {
            PlayerList[index] = new Player();
            OnPropertyChanged("PlayerList[" + index + "].Name");
        }

        private void UpdateOtherPlayerTile(int playerID, int domino, int side, int x, int y)
        {
            Tile newTile;
            if (side == 1)
            {
                newTile = dominoHolder.SpecificDomino(domino).Tile1;
            }
            else
            {
                newTile = dominoHolder.SpecificDomino(domino).Tile2;
            }
            PlayerList[playerID].Board.Add(newTile, x, y);
        }

        public void DisplayChatMessage(int index, string text)
        {
            ChatHistory += PlayerList[index].Name + ": " + text + "\n";
            OnPropertyChanged("ChatHistory");
        }
    }
}