﻿using ArtWarsClientWPF.Models;
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
        private List <DrawingPacket> drawingPacketSend;
        public VotingWindow(TcpHandler tcpHandler, Client client, DrawingPacket firstPacketForVote)
        {
            _handler = tcpHandler;
            _client = client;
            drawingPacketSend = new List<DrawingPacket>();
            drawingPacketSend.Add(firstPacketForVote);
            InitializeComponent();
      
            _ = ReceiveImagesFromServerAsync();
        }
        private async Task ReceiveImagesFromServerAsync()
        {
            //could be less or more than four images

            //_images = new BitmapImage[4];
            for (int i = 0; i < 4; i++)
            {
                byte[] data = new byte[4048];
                int bytes = await _handler._stream.ReadAsync(data, 0, data.Length);
                DrawingPacket drawingPacket = new DrawingPacket(data, _client);
                drawingPacketSend.Add(drawingPacket);

                //BitmapImage image = new BitmapImage();
                //image.BeginInit();
                //image.StreamSource = new MemoryStream(drawingPacket.image);
                //image.EndInit();
                //_images[i] = image;
                //if (_client.state == "Voting")
                //{
                //    //do not accept anymore
                //    break;
                //}
            }
            //Displays first image once its been received
            updateImage();
        }
        private void updateImage()
        {
            DrawingPacket currentPacket;
            //for(int i = 0; i < 4; i++)
            //{
            //    if (i == _currentImageIndex)
            //    {
            //        currentPacket = drawingPacketSend[_currentImageIndex];
            //    }
            //}
            //scroll through the images
            currentPacket = drawingPacketSend[_currentImageIndex];
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(currentPacket.image);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            //if (_client.state == "Voting")
            //{
            //    //do not accept anymore
            //    break;
            //}

            //fixed if condition from == to !=
            if (image != null && drawingPacketSend != null && drawingPacketSend.Count > _currentImageIndex)
            {
                Image1.Source = image;
            }

        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if(_currentImageIndex > 0)
            {
                _currentImageIndex--;
                updateImage();
            }
            
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentImageIndex < drawingPacketSend.Count - 1)
            {
                _currentImageIndex++;
                updateImage();
            }

        }

        private void VoteButton_Click(object sender, RoutedEventArgs e)
        {
           if (drawingPacketSend == null || drawingPacketSend.Count == 0)
            {
                MessageBox.Show("No images to vote for currently.");
                return;
            }

           int votedImageIndex = _currentImageIndex;
            byte[] data = drawingPacketSend[votedImageIndex].Serialize();

            _handler._stream.Read(data, 0, data.Length);

            // Disable the vote button to stop vote button from being pressed multiple times
            VoteButton.IsEnabled = false;
            MessageBox.Show($"Vote successful");
            //got to ResultsWindow
            ResultWindow resultsWindow = new ResultWindow(_handler, _client);
            resultsWindow.Show();
            this.Close();
        }
    }
}
