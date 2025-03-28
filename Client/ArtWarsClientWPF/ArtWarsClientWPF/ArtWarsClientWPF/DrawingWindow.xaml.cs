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
using System.ComponentModel;

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

        // Add a property for DrawingPrompt
        public string DrawingPrompt;

        public DrawingWindow(TcpHandler handler, Client client, dynamic packet)
        {
            _handler = handler;
            _client = client;
            _packet = packet;
            InitializeComponent();
            Background = Brushes.White;
            DrawingPrompt = "Draw you master pice";// packet.prompt;

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
            if (CanvasArea != null)
            {
                CanvasArea.SetBrushThickness(e.NewValue);
            }
            else
            {
                // Handle the null case, e.g., log an error or initialize CanvasArea
            }
        }

    }
}

