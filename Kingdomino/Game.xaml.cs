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
        private readonly ViewModel viewModel;
        private Image[,] images;

        public Game()
        {
            viewModel = new ViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
            images = new Image[5, 5];
            CreateImageList();
        }

        private void Spot_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (((Button)sender).Name.Substring(0, ((Button)sender).Name.Length - 6).Equals(images[i,j].Name))
                    {
                        viewModel.UpdatePlacedTile(i, j);
                    }
                }
            }
        }

        public void EndGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(viewModel.CalculateWinner());
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateChosenTile(Int32.Parse(((Button) sender).Name.Substring(12,1)));
        }

        private void Board_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SwitchBoardView(Int32.Parse(((Button)sender).Name.Substring(((Button)sender).Name.Length - 1)) - 1);
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateChosenDomino(Int32.Parse(sender.ToString().Substring(sender.ToString().Length - 1)) - 1);
        }
        private void CreateImageList()
        {
            images[0, 0] = One_One;
            images[0, 1] = One_Two;
            images[0, 2] = One_Three;
            images[0, 3] = One_Four;
            images[0, 4] = One_Five;
            images[1, 0] = Two_One;
            images[1, 1] = Two_Two;
            images[1, 2] = Two_Three;
            images[1, 3] = Two_Four;
            images[1, 4] = Two_Five;
            images[2, 0] = Three_One;
            images[2, 1] = Three_Two;
            images[2, 2] = Three_Three;
            images[2, 3] = Three_Four;
            images[2, 4] = Three_Five;
            images[3, 0] = Four_One;
            images[3, 1] = Four_Two;
            images[3, 2] = Four_Three;
            images[3, 3] = Four_Four;
            images[3, 4] = Four_Five;
            images[4, 0] = Five_One;
            images[4, 1] = Five_Two;
            images[4, 2] = Five_Three;
            images[4, 3] = Five_Four;
            images[4, 4] = Five_Five;
        }
    }
}
