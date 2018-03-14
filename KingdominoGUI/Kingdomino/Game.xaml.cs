using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KingDomino
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
            DominoHolder dominoHolder = new DominoHolder();
            Domino domino1 = dominoHolder.RandomDomino();
            Domino1.Source = new BitmapImage(new Uri(domino1.DominoBack, UriKind.Relative));
            Tile1.Source = new BitmapImage(new Uri(domino1.Tile1.TileImage, UriKind.Relative));
            Tile2.Source = new BitmapImage(new Uri(domino1.Tile2.TileImage, UriKind.Relative));

            Domino domino2 = dominoHolder.RandomDomino();
            Domino2.Source = new BitmapImage(new Uri(domino2.DominoBack, UriKind.Relative));
            Tile3.Source = new BitmapImage(new Uri(domino2.Tile1.TileImage, UriKind.Relative));
            Tile4.Source = new BitmapImage(new Uri(domino2.Tile2.TileImage, UriKind.Relative));

            Domino domino3 = dominoHolder.RandomDomino();
            Domino3.Source = new BitmapImage(new Uri(domino3.DominoBack, UriKind.Relative));
            Tile5.Source = new BitmapImage(new Uri(domino3.Tile1.TileImage, UriKind.Relative));
            Tile6.Source = new BitmapImage(new Uri(domino3.Tile2.TileImage, UriKind.Relative));

            Domino domino4 = dominoHolder.RandomDomino();
            Domino4.Source = new BitmapImage(new Uri(domino4.DominoBack, UriKind.Relative));
            Tile7.Source = new BitmapImage(new Uri(domino4.Tile1.TileImage, UriKind.Relative));
            Tile8.Source = new BitmapImage(new Uri(domino4.Tile2.TileImage, UriKind.Relative));
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
