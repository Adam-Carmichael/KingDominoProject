using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        public ObservableCollection<Domino> NextDominos { get; set; }
        public ObservableCollection<Domino> CurrentDominos { get; set; }
        public Board CurrentBoard { get; set; }
        public Domino ChosenDomino { get; set; }
        public Tile ChosenTile { get; set; }

        public string Score { get; set; }

        private int roundNumber = 1;

        private DominoHolder dominoHolder = new DominoHolder();

        private void createPlayers()
        {
            for (int i = 0; i < 4; i++)
            {
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

        public void SwitchBoardView(int index)
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

        public void SetCurrentDominosFromNextDominos()
        {
            int index = 0;
            foreach (Domino domino in NextDominos)
            {
                CurrentDominos[index] = domino;
                OnPropertyChanged("CurrentDominos[" + index + "]");
                ++index;
            }
        }

        private void getFourRandomDominos(ObservableCollection<Domino> dominos)
        {
            for (int i = 0; i < 4; i++)
            {
                dominos[i] = dominoHolder.RandomDomino();
            }
        }


        public void CreateBackFacingDominos()
        {
            getFourRandomDominos(NextDominos);

            sortDominos(NextDominos);

            for (int i = 0; i < 4; i++)
            {
                OnPropertyChanged("NextDominos[" + i + "]");
            }

        }

        private void sortDominos(ObservableCollection<Domino> dominos)
        {
            int size = dominos.Count;
            for (int j = size - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    if (dominos[i].Number > dominos[i + 1].Number)
                        exchange(dominos, i, i + 1);
                }
            }
        }

        private void exchange(ObservableCollection<Domino> dominos, int m, int n)
        {
            Domino tempDomino;

            tempDomino = dominos[m];
            dominos[m] = dominos[n];
            dominos[n] = tempDomino;
        }

        // Refreshs the four starting dominos and flips the next ones
        public void RefreshSelectionDominos()
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
        }
    }
}
