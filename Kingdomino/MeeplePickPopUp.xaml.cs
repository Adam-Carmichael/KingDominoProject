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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KingDomino
{
    /// <summary>
    /// Interaction logic for MeeplePickPopUp.xaml
    /// </summary>
    public partial class MeeplePickPopUp : Page
    {
        private readonly ViewModel viewModel;

        public MeeplePickPopUp()
        {
            viewModel = new ViewModel();
            InitializeComponent();
        }

        private void MeepleSelectButton_Click(object sender, RoutedEventArgs e)
        {
            string meepleColor = ((Button)sender).Name;
            // ToDo make Switch statement
            viewModel.InitPlayerMeeples(meepleColor);
        }
    }
}
