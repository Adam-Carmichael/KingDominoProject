using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DataModel;
using PeerManager;

namespace WpfMockup
{
    // Methods in this class are called by the message handler, updating the actions of remote clients
    public class ExampleViewModel : INotifyPropertyChanged
    {
        private IMessenger _msgHandler;
        /*
         *
         * Elements from the view are bound to these properties
         *
         */
        public string InitName { get; set; } = "PlayerNameHere";                    // used for inital name setting
        public string InitColor { get; set; } = "Color";                            // used for initial color setting
        public string ChatHistory { get; set; } = "Welcome!\n";                     // chat textbox
        public ObservableCollection<PlayerData> PlayerList { get; set; }            // Collection of connected players
        public int ThisPlayer { get; set; }                                         // index in PlayerList of this client
        
        public ExampleViewModel()
        {
            PlayerList = new ObservableCollection<PlayerData> {null, null, null, null, null};
            UpdatePlayerData(0, true, "System", "");
            UpdatePlayerData(1, false, "Player1", "Pink");
            UpdatePlayerData(2, false, "Player2", "Blue");
            UpdatePlayerData(3, false, "Player3", "Green");
            UpdatePlayerData(4, false, "Player4", "Yellow");
        }

        public IMessenger InitComm(bool host)
        {
            _msgHandler = new Messenger(host, ReceiveMessage);
            return _msgHandler;
        }

        /*
         *
         *  The following methods update Properties for the gui
         *
         */

        public void DisplayChatMessage(int index, string text)
        {
            ChatHistory += PlayerList[index].Name + ": " + text + "\n";
            OnPropertyChanged("ChatHistory");
        }

        public void UpdatePlayerData(int index, string name, string color)
        {
            PlayerList[index].Name = name;
            PlayerList[index].Color = color;
            OnPropertyChanged("PlayerList[" + index + "].Name");
            OnPropertyChanged("PlayerList[" + index + "].Color");
        }

        // 
        public void UpdatePlayerData(int index, bool isFull, string name, string color)
        {
            PlayerList[index] = new PlayerData(isFull, name, color, new Tile("grass", 0));
            OnPropertyChanged("PlayerList[" + index + "].Name");
            OnPropertyChanged("PlayerList[" + index + "].Color");
        }

        /*
         *
         *  The following methods enable remote client updates to be reflected locally
         *
         */

        // establishes the identity of this client
        public void InitThisPlayer(int playerNum, string name, string color)
        {
            ThisPlayer = playerNum;
            PlayerList[ThisPlayer].IsOccupied = true;
            UpdatePlayerData(playerNum, true, name, color);
            _msgHandler.SendPlayerUpdate(ThisPlayer, PlayerList[ThisPlayer]);
            ChatHistory += String.Format("Hi, {0}! You have joined as Player {1}\n", name, playerNum);
            OnPropertyChanged("ChatHistory");
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
                    ThisPlayer = message.PeerId;
                    InitThisPlayer(ThisPlayer, InitName, InitColor);
                    break;
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
                    UpdatePlayerData(message.PeerId, message.PlayerName, message.Color);
                    break;
                default:
                    DisplayChatMessage(0, "Error: Network message not recognized");
                    break;
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
    }
}
