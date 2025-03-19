using System.Windows;
using System.Windows.Media;

namespace ArtWarsClientWPF
{
    public partial class DrawingWindow : Window
    {
        public DrawingWindow()
        {
            InitializeComponent();
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

