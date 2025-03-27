using ArtWarsClientWPF.Models;
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
    /// Interaction logic for DrawingWindow.xaml
    /// </summary>
    public partial class DrawingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;
        private dynamic _packet;

  
        public DrawingWindow(TcpHandler handler, Client client, dynamic packet)
        {
            _handler = handler;
            _client = client;
            _packet = packet;
            InitializeComponent();
            Background = Brushes.White;
         
        }
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button && button.Tag is string colorName)
            {
                CanvasArea.SetBrushColour((Brush)new BrushConverter().ConvertFromString(colorName));
            }
        }

        private void Erase_Click(object sender, RoutedEventArgs e)
        {
            CanvasArea.TurnOnEraser();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CanvasArea.ClearCanvas();
        }

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CanvasArea.SetBrushThickness(e.NewValue);
        }
    }
}

