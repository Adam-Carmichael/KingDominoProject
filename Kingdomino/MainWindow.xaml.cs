using System.Windows;
using PeerManager;

namespace KingDomino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainMenuModel mmModel;
        private bool host;
        private string PlayerName { get; set; }
        private ViewModel model;
        private IMessenger _msgHandler;
        
        public MainWindow()
        {
            mmModel = new MainMenuModel();
            this.DataContext = mmModel;
            InitializeComponent();

            // setting up view model for game ahead of time
            model = new ViewModel();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = NameInput.Text;
            Hide();

            _msgHandler.Start();
            Game game = new Game(model, host, Name);
            game.Show();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            mmModel.ButtonsVisible = Visibility.Hidden;
            mmModel.NameInputVisible = Visibility.Visible;
            host = true;
            _msgHandler = model.InitComm(host);
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            mmModel.ButtonsVisible = Visibility.Hidden;
            mmModel.NameInputVisible = Visibility.Visible;
            host = false;
            _msgHandler = model.InitComm(host);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
