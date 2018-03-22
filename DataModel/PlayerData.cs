using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace DataModel
{
    public class PlayerData : INotifyPropertyChanged
    {
        public PlayerData(bool isFull, string name, string color, Tile tile)
        {
            IsOccupied = isFull;
            Name = name;
            Color = color;
            Tile = tile;
        }

        private bool _isOccupied;
        public bool IsOccupied
        {
            get { return _isOccupied; }
            set
            {
                _isOccupied = value;
                OnPropertyChanged("IsOccupied");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        private Tile _tile;
        public Tile Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;
                OnPropertyChanged("Tile");
            }
        }

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
