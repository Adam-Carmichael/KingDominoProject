using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfWcfExample
{
    public class PlayerData
    {
        public PlayerData(string name, string color)
        {
            Name = name;
            Color = color;
        }
        public string Name { get; set; }
        public string Color { get; set; }
    }

    // Methods in this class are called by the message handler, updating the actions of p2p clients
    public class ExampleViewModel : INotifyPropertyChanged
    {
        // Elements from the view are bound to these properties
        public string Name { get; set; } = "Player1";
        public string ChatHistory { get; set; } = "Welcome!\n";

        // Collection of connected players
        public PlayerData Player1 { get; set; }
        public PlayerData Player2 { get; set; }
        public PlayerData Player3 { get; set; }
        public PlayerData Player4 { get; set; }
        public List<PlayerData> Players;
        public PlayerData ThisPlayer { get; set; }

        public void Init()
        {
            Player1 = new PlayerData("Player1", "pink");
            Player2 = new PlayerData("Player2", "pink");
            Player3 = new PlayerData("Player3", "pink");
            Player4 = new PlayerData("Player4", "pink");
            Players = new List<PlayerData> {null, Player1, Player2, Player3, Player4};
            ThisPlayer = Player1;
        }
        
        public void DisplayChatMessage(int player, string text)
        {
            if (player == 9)
            {
                ChatHistory += "System " + text + "\n";
            }
            else
            {
                ChatHistory += Players[player].Name + ": " + text + "\n";
            }
            OnPropertyChanged("ChatHistory");
        }

        public void UpdatePlayerColor(int player, string color)
        {
            switch (player)
            {
                case 1:
                    Player1.Color = color;
                    break;
                case 2:
                    Player2.Color = color;
                    break;
                case 3:
                    Player3.Color = color;
                    break;
            }
            OnPropertyChanged("Player" + player + ".Color");
        }

        // OnPropertyChanged must be called to tell a view bound to this implementation to get updated property
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
