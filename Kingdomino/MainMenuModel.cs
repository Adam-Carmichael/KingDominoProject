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

namespace KingDomino
{
    class MainMenuModel
    {
        public Visibility ButtonsVisible { get; set; }
        public Visibility NameInputVisible { get; set; }

        public MainMenuModel()
        {
            ButtonsVisible = Visibility.Visible;
            NameInputVisible = Visibility.Hidden;
        }
    }
}
