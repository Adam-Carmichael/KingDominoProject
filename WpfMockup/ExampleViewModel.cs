using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DataModel;
using PeerManager;

namespace WpfMockup
{
    // Methods in this class are called by the message handler, updating the actions of p2p clients
    public class ExampleViewModel : INotifyPropertyChanged
    {
        // Elements from the view are bound to these properties
        public string ChatHistory { get; set; } = "Welcome!\n";                     // textbox
        public ObservableCollection<PlayerData> PlayerList { get; set; }            // Collection of connected players

        private int _thisPlayer;

        public ExampleViewModel()
        {
            PlayerList = new ObservableCollection<PlayerData>() {null, null, null, null, null};
            UpdatePlayerData(0, true, "System", "");
            UpdatePlayerData(1, false, "Player1", "Pink");
            UpdatePlayerData(2, false, "Player2", "Blue");
            UpdatePlayerData(3, false, "Player3", "Green");
            UpdatePlayerData(4, false, "Player4", "Yellow");

            OnPropertyChanged(null);                    // null indicates OnPropertyChanged should update all properties
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

        public void UpdatePlayerData(int index, PlayerData player)
        {
            PlayerList[index] = player;
            DisplayChatMessage(0, "Updated Player" + index);
            DisplayChatMessage(0, "Player's Tile was " + player.Tile.Terrain + "with " + player.Tile.Crowns + " crowns.");
        }

        public void UpdatePlayerData(int index, bool isFull, string name, string color)
        {
            PlayerList[index] = new PlayerData(isFull, name, color, new Tile("grass", 0));
            DisplayChatMessage(0, "Updated Player" + index);
        }

        public void ReceiveMessage(SerializedMessage message)
        {
            switch (message.Purpose)
            {
                case Purpose.Chat:
                    DisplayChatMessage(message.PeerId, message.Text);
                    break;
                case Purpose.Deal:
                    break;
                case Purpose.Select:
                    break;
                case Purpose.Tile:
                    break;
                case Purpose.Player:
                    UpdatePlayerData(message.PeerId, message.Player);
                    break;
                case Purpose.System:
                    SystemMessageHandler(message);
                    break;
                default:
                    DisplayChatMessage(0, "Error: Network message not recognized");
                    break;
            }
        }

        private void SystemMessageHandler(SerializedMessage message)
        {
            switch (message.Text)
            {
                case "rollcall":
                    if (_thisPlayer != 1) break;
                    for (int i = 1; i <= 4; i++)        // TODO eliminate Magic Numbers
                    {
                        PlayerData source = PlayerList[i];
                        SerializedMessage data = new SerializedMessage(Purpose.Player, i)
                        {
                            Player = source
                        };
                        _peerService.SendMessage(data);
                        _viewModel.DisplayChatMessage(0, "Sending data for Player" + i);
                    }
                    break;
            }
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
