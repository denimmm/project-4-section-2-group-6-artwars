using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
namespace ArtWarsClientWPF
{
    public class DrawingCanvas : Canvas
    {
        private Polyline polyline;
        private Brush currentBrush = Brushes.Black;
        private double brushThickness = 3;

        public DrawingCanvas()
        {
            Background = Brushes.White;
            MouseDown += Canvas_MouseDown;
            MouseMove += Canvas_MouseMove;
            MouseUp += Canvas_MouseUp;
        }

        public void SetBrushColour(Brush brush)
        {
            currentBrush = brush;
        }

        public void TurnOnEraser()
        {
            currentBrush = Brushes.White;
        }

        public void SetBrushThickness(double thickness)
        {
            brushThickness = thickness;
        }

        public void ClearCanvas()
        {
            Children.Clear();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            polyline = new Polyline
            {
                Stroke = currentBrush,
                StrokeThickness = brushThickness
            };
            polyline.Points.Add(e.GetPosition(this));
            Children.Add(polyline);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && polyline != null)
            {
                polyline.Points.Add(e.GetPosition(this));
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            polyline = null;
        }
        // Add a method to save the canvas to a file
        public void SaveCanvas(string filename, int quality = 90)
        {
            var renderBitmap = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(this);

            var encoder = new JpegBitmapEncoder();
            encoder.QualityLevel = quality;
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
    }
}

