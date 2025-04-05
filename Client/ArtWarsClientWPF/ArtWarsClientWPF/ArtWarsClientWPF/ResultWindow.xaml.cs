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
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;
        public ResultWindow(TcpHandler handler, Client client)
        {
            _handler = handler;
            _client = client;
            InitializeComponent();
            //start receiving the result image from server
            _ = ReceiveResultImageFromServerAsync();
        }
        //wait to receive the result image from server
        private async Task ReceiveResultImageFromServerAsync()
        {
            try
            {
                byte[] data = new byte[4048];
                int bytes = await _handler._stream.ReadAsync(data, 0, data.Length);

                if (bytes > 0)
                {
                    DrawingPacket drawingPacket = new DrawingPacket(data);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(drawingPacket.image);
                    image.CacheOption = BitmapCacheOption.OnLoad; // Optional but safe
                    image.EndInit();
                    image.Freeze(); // Makes it UI-thread safe

                    WinnerImage.Source = image;
                }
                else
                {
                    MessageBox.Show("Failed to load winner image. Server returned no data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving result image: {ex.Message}");
            }
        }

        //close the window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
