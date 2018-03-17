using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;

namespace WpfMockup
{
    // Main should only contain listeners and initializer, no data implementation

    public partial class MainWindow : Window
    {
        private readonly IMessageHandler _msgHandler;
        private string InitName { get; set; } = "PlayerNameHere";
        private string InitColor { get; set; }= "Color";

        public MainWindow()
        {
            // set up MVVC
            ExampleViewModel model = new ExampleViewModel();
            DataContext = model;
            InitializeComponent();

            // pass viewmodel to the p2p message handler so it can update the view
            _msgHandler = new MessageHandler(model);
        }

        // establish new session
        private void NewSession_Clicked(object sender, RoutedEventArgs e)
        {
            _msgHandler.NewGame(InitName, InitColor);
        }

        // join existing session
        private void Connect_Clicked(object sender, RoutedEventArgs e)
        {
            _msgHandler.JoinGame(InitName, InitColor);
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
