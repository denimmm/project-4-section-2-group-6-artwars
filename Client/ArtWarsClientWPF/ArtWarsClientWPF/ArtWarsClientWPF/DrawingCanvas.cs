using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Ink;
using System.Windows;

namespace ArtWarsClientWPF
{
    public class DrawingCanvas : Canvas
    {
        private Polyline polyline;
        private bool isDrawing = false;
        private Brush currentBrush = Brushes.Black;
        private double brushThickness = 3;
        private bool isEraser = false;


        public DrawingCanvas()
        {
            Background = Brushes.White;
            MouseMove += Canvas_MouseMove;
        }

        public void SetBrushColour(Brush brush)
        {
            currentBrush = brush;
            isEraser = false;
        }

        public void TurnOnEraser()
        {
            currentBrush = Brushes.White;
            isEraser = true;
        }

        public void SetBrushThickness(double thickness)
        {
            brushThickness = thickness;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (polyline == null)
                {
                    polyline = new Polyline
                    {
                        Stroke = currentBrush,
                        StrokeThickness = brushThickness
                    };
                    Children.Add(polyline);
                }
                polyline.Points.Add(e.GetPosition(this));
            }
            else
            {
                polyline = null;
            }
            }
        public void ClearCanvas()
        {
            Children.Clear();
        }
        
        }
    }
