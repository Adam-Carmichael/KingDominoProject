using System.Windows;
using PeerManager;

namespace KingDomino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private bool host;
        private string PlayerName { get; set; }
        private IMessenger _msgHandler;
        
        public MainWindow()
        {
            viewModel = new ViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = NameInput.Text;
            Hide();

            _msgHandler.Start();
            Game game = new Game(viewModel, PlayerName);
            game.Show();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ButtonsVisible = Visibility.Hidden;
            viewModel.NameInputVisible = Visibility.Visible;
            host = true;
            _msgHandler = viewModel.InitComm(host);
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ButtonsVisible = Visibility.Hidden;
            viewModel.NameInputVisible = Visibility.Visible;
            host = false;
            _msgHandler = viewModel.InitComm(host);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
