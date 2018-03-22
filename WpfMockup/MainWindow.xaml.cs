using System;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PeerManager;

namespace WpfMockup
{
    // Main should only contain listeners and initializer, no data implementation

    public partial class MainWindow : Window
    {
        private IMessenger _msgHandler;
        private string InitName { get; set; } = "PlayerNameHere";
        private string InitColor { get; set; }= "Color";

        public MainWindow()
        {
            // set up MVVC
            ExampleViewModel model = new ExampleViewModel();
            DataContext = model;
            InitializeComponent();
        }

        // establish new session
        private void NewSession_Clicked(object sender, RoutedEventArgs e)
        {
            ExampleViewModel model = (ExampleViewModel)DataContext;
            Connection connection = Connection.CreateConnection(true);

            IPeerService peerService = connection.Start(model.ReceiveMessage);
            _msgHandler = new HostMessenger(model, peerService);
        }

        // join existing session
        private void Connect_Clicked(object sender, RoutedEventArgs e)
        {
            ExampleViewModel model = (ExampleViewModel)DataContext;
            Connection connection = Connection.CreateConnection(false);

            IPeerService peerService = connection.Start(model.ReceiveMessage);
            _msgHandler = new PeerMessenger(model, peerService);
        }

        // send chat message
        private void InputBox_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                _msgHandler.SendChatMessage(InputBox.Text);
                InputBox.Clear();
            }
        }

        private void RadioColor_Clicked(object sender, RoutedEventArgs e)
        {
            RadioButton source = (RadioButton)e.Source;
            InitColor = source.Content.ToString();
        }

        // change player name
        private void PlayerName_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox source = (TextBox) e.Source;
            InitName = source.Text;
        }
    }
}
