using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfMockup
{
    // Methods in this class are called by the message handler, updating the actions of p2p clients
    public class ExampleViewModel : INotifyPropertyChanged
    {
        // Elements from the view are bound to these properties
        public string ChatHistory { get; set; } = "Welcome!\n";

        // Collection of connected players
        public ObservableCollection<PlayerData> PlayerList { get; set; }

        public ExampleViewModel()
        {
            PlayerList = new ObservableCollection<PlayerData>() {null, null, null, null, null };
            UpdatePlayerData(0, true, "System", "");
            UpdatePlayerData(1, false, "Player1", "Pink");
            UpdatePlayerData(2, false, "Player2", "Blue");
            UpdatePlayerData(3, false, "Player3", "Green");
            UpdatePlayerData(4, false, "Player4", "Yellow");

            OnPropertyChanged(null);
        }

        public void InitThisPlayer(int playerNum, string name, string color)
        {
            ChatHistory += String.Format("Hi, {0} You have joined as Player {1}\n", name, playerNum);
            OnPropertyChanged("ChatHistory");
            UpdatePlayerData(playerNum, true, name, color);
        }

        
        public void DisplayChatMessage(int index, string text)
        {
            ChatHistory += PlayerList[index].Name + ": " + text + "\n";
            OnPropertyChanged("ChatHistory");
        }

        public void UpdatePlayerData(int index, bool isFull, string name, string color)
        {
            PlayerList[index] = new PlayerData(isFull, name, color);
        }

        // OnPropertyChanged must be called to tell a view bound to this implementation to get updated property
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
