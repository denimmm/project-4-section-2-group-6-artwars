using ArtWarsClientWPF.Models;
using ArtWarsClientWPF.StatePacket;
using ArtWarsServer.Model;
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
    public partial class VotingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;
        int packetCount = 0;
        private int _currentImageIndex = 0;
        private List <DrawingPacket> drawingPacketsReceived = new List<DrawingPacket>();
        public VotingWindow(TcpHandler tcpHandler, Client client, DrawingPacket firstPacketForVote)
        {
            _handler = tcpHandler;
            _client = client;
            //drawingPacketsReceived = new List<DrawingPacket>();
            drawingPacketsReceived.Add(firstPacketForVote);
            InitializeComponent();
      
            _ = ReceiveImagesFromServerAsync();

            //updateImage();
        }
        private async Task ReceiveImagesFromServerAsync()
        {

          bool isAllReceived = false;
            //check if the packet in list is last packet
            if(drawingPacketsReceived != null && drawingPacketsReceived.Count > 0)
            {
                packetCount++;
                DrawingPacket lastPacket = drawingPacketsReceived[drawingPacketsReceived.Count - 1];
                if (lastPacket.type == "Voting")
                {
                    isAllReceived = true;
                }
            }
            while (!isAllReceived){
                byte[] data = new byte[2*1024*1024];
                int bytes = await _handler._stream.ReadAsync(data, 0, data.Length);
                if (bytes > 0)
                {
                    // Deserialize the packet
                    DrawingPacket drawingPacket = new DrawingPacket(data);
                    drawingPacketsReceived.Add(drawingPacket);
                    packetCount++;
                    // Check if the packet is a drawing packet
                    if(drawingPacket.type == "Voting")
                    {
                        _client.state = drawingPacket.type;
                        isAllReceived = true;
                    }
                    
                }
                
          }
          _client.state = "Voting";
            //Displays first image once its been received
            updateImage();
        }
        private void updateImage()
        {


            if (drawingPacketsReceived != null && packetCount > _currentImageIndex)
            {

                DrawingPacket currentPacket = drawingPacketsReceived[_currentImageIndex];
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(currentPacket.image);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();

                //fixed if condition from == to !=
                if (image != null && drawingPacketsReceived != null && packetCount > _currentImageIndex)
                {
                    Image1.Source = image;
                }
            }

        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if(_currentImageIndex > 0)
            {
                _currentImageIndex--;
                updateImage();
            }
            //else { MessageBox.Show("No more images to the left.");
            //}
            
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentImageIndex < packetCount - 1)
            {
                _currentImageIndex++;
                updateImage();
            }
            //else { MessageBox.Show("No more images to the right.");
            //}

        }

        private void VoteButton_Click(object sender, RoutedEventArgs e)
        {
           if (drawingPacketsReceived == null || drawingPacketsReceived.Count == 0)
            {
                MessageBox.Show("No images to vote for currently.");
                return;
            }

           int votedImageIndex = _currentImageIndex;
            //byte[] data = drawingPacketsReceived[votedImageIndex].Serialize();

            // Send the vote to the server
            VotingPacket votePacket = new VotingPacket(_client.roomCode, drawingPacketsReceived[votedImageIndex].playerId, int.Parse(_client.player.Id));
            byte[] data = votePacket.Serialize();

            _handler._stream.Write(data, 0, data.Length);
            
            // Disable the vote button to stop vote button from being pressed multiple times
            VoteButton.IsEnabled = false;
            //MessageBox.Show($"You voted for {drawingPacketsReceived[_currentImageIndex].playerId}");
            //got to ResultsWindow
            ResultWindow resultsWindow = new ResultWindow(_handler, _client);
            resultsWindow.Show();
            this.Close();
        }
    }
}
