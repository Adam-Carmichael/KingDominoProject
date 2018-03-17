using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfMockup
{
    public class PlayerData : INotifyPropertyChanged
    {
        public PlayerData(bool isFull, string name, string color)
        {
            IsOccupied = isFull;
            Name = name;
            Color = color;
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
