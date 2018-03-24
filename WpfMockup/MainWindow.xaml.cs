using System;
using System.Windows;
using System.Windows.Input;
using PeerManager;

namespace WpfMockup
{
    /*
     * Main should only contain listeners and initializer, no data implementation
     *
     * This is still poor MVVC implementation!
     * Listeners should activate ICommand instances, which would handle the implementation
     * ICommands would then interact with the Messenger and ViewModel
     */

    public partial class MainWindow : Window
    {
        private readonly ExampleViewModel _model;
        private Connection _connection;
        private IPeerService _peerSvc;
        private IMessenger _msgHandler;

        // Entry Point
        public MainWindow()
        {
            // set up MVVC
            _model = new ExampleViewModel();
            DataContext = _model;
            InitializeComponent();
        }

        // establish new session: create cnnection, start connection, and pass connection to handler
        private void NewSession_Clicked(object sender, RoutedEventArgs e)
        {
            _connection = Connection.CreateConnection(true);            // only difference true/false
            _peerSvc = _connection.Start(_model.ReceiveMessage);
            _msgHandler = new Messenger(_peerSvc);
        }

        // join existing session
        private void Connect_Clicked(object sender, RoutedEventArgs e)
        {
            _connection = Connection.CreateConnection(false);            // only difference true/false
            _peerSvc = _connection.Start(_model.ReceiveMessage);
            _msgHandler = new Messenger(_peerSvc);
        }

        // send chat message
        private void InputBox_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                _msgHandler.SendChatMessage(_model.ThisPlayer, InputBox.Text);
                InputBox.Clear();
            }
        }
    }
}
