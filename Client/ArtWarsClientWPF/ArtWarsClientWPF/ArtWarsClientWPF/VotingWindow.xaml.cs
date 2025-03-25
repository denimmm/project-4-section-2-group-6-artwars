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

namespace ArtWarsClientWPF
{
    /// <summary>
    /// Interaction logic for VotingWindow.xaml
    /// </summary>
    public partial class VotingWindow : Window
    {
        public VotingWindow()
        {
            InitializeComponent();
        }
        private void updateImage()
        {

        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            updateImage();
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            updateImage();
        }
    }
}
