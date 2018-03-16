using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfWcfExample
{
    // Main should only contain listeners and initializer, no data implementation

    public partial class MainWindow : Window
    {
        private readonly IMessageHandler _msgHandler;

        public MainWindow()
        {
            // set up MVVC
            DataContext = new ExampleViewModel();
            InitializeComponent();

            // pass viewmodel to the p2p message handler so it can update the view
            ExampleViewModel model = (ExampleViewModel) DataContext;
            _msgHandler = new MessageHandler(model);

            this.InvalidateVisual();
        }

        // establish new session
        private void NewSession_Clicked(object sender, RoutedEventArgs e)
        {
        }

        // join existing session
        private void Connect_Clicked(object sender, RoutedEventArgs e)
        {
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

        // change player color
        private void RadioColor_Changed(object sender, RoutedEventArgs e)
        {
            RadioButton source = (RadioButton)e.Source;
            _msgHandler.SendPlayerColor(source.Name);
        }
    }
}
