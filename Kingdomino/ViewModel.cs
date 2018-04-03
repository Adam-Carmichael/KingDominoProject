using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino
{
    class ViewModel : INotifyPropertyChanged
    {
        private string chatHistory;
        public string ChatHistory
        {
            get { return chatHistory; }
            set { chatHistory = value; }
        }
        public ObservableCollection<Player> PlayerList { get; set; }
        public ObservableCollection<Domino> NextDomino { get; set; }
        public ObservableCollection<Domino> CurrentDomino { get; set; }
        public Board CurrentBoard { get; set; }
        public Domino ChosenDomino { get; set; }
        public Tile ChosenTile { get; set; }

        public string Score { get; set; }


        private void createPlayers()
        {
            for (int i = 0; i < 4; i++) {
                PlayerList[i] = new Player();
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
            OnPropertyChanged("CurrentBoard[" + x + "][" + y + "]");
        }

        public void switchBoardView(int index)
        {
            CurrentBoard = PlayerList[index].Board;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    OnPropertyChanged("CurrentBoard[" + i + "][" + j + "]");
                }
            }
        }

        // INotifyPropertyChanged: 
        // OnPropertyChanged must be called to tell a view bound to this implementation to get specified updated property
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnDominoSelect(int index)
        {

        }

        public void UpdateScores()
        {
            foreach (Player player in PlayerList)
            {
                player.Board.CalculateScore();
            }

            OnPropertyChanged("Score");
        }
    }
}
