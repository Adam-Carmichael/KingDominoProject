using System;
using System.ComponentModel;

namespace WpfWcfExample
{
    // Methods in this class are called by the message handler, updating the actions of p2p clients

    public class ExampleViewModel : INotifyPropertyChanged
    {
        // Elements from the view are bound to these properties
        public string Name { get; set; } = "Player1";
        public string ChatHistory { get; set; } = "Welcome!\n";
        public string Player1Color { get; set; } = "Pink";
        public string Player2Color { get; set; } = "Blue";
        public string Player3Color { get; set; } = "Green";

        public ExampleViewModel() { }
        
        public void DisplayChatMessage(string user, string text)
        {
            ChatHistory += (user + ": " + text + "\n");
            OnPropertyChanged("ChatHistory");
        }

        public void UpdatePlayerColor(string player, string color)
        {
            switch (player)
            {
                case "Player1":
                    Player1Color = color;
                    break;
                case "Player2":
                    Player2Color = color;
                    break;
                case "Player3":
                    Player3Color = color;
                    break;
            }
            OnPropertyChanged(player + "Color");
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
