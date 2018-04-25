using System.Windows;

namespace KingDomino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {

            Hide();
            Game game = new Game(false);
            game.Show();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Game game = new Game(true);
            game.Show();
        }
    }
}
