using ArtWarsClientWPF.Models;
using ArtWarsClientWPF.StatePacket;
using System;
using System.Collections.Generic;
using System.IO;
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
        private Client _client;
        private TcpHandler _handler;
        private BitmapImage[] _images;
        private int _currentImageIndex = 0;
        public VotingWindow(TcpHandler tcpHandler, Client client)
        {
            _handler = tcpHandler;
            _client = client;
            InitializeComponent();
      
            _ = ReceiveImagesFromServerAsync();
        }
        private async Task ReceiveImagesFromServerAsync()
        {
            //could be less or more than four images

            _images = new BitmapImage[4];
            for (int i = 0; i < 4; i++)
            {
                byte[] data = new byte[4048];
                int bytes = await _handler._stream.ReadAsync(data, 0, data.Length);
                DrawingPacket drawingPacket = new DrawingPacket(data, _client);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(drawingPacket.image);
                image.EndInit();
                _images[i] = image;
                if (_client.state == "Voting")
                {
                    //do not accetp anymore
                    break;
                }
            }
        }
        private void updateImage()
        {
            //sroll through the images
            if(_images != null && _images.Length >0)
            {
                //view current image
                Image1.Source = _images[_currentImageIndex];
            }
        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if(_currentImageIndex > 0 && _images !=null)
            {
                _currentImageIndex--;
                updateImage();
            }
            
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentImageIndex < _images.Length - 1 && _images == null)
            {
                _currentImageIndex++;
                updateImage();
            }

        }
    }
}
