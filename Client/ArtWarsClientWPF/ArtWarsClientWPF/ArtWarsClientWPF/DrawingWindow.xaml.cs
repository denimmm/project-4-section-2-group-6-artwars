﻿using ArtWarsClientWPF.Models;
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
using System.IO;
using ArtWarsClientWPF.StatePacket;

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
        private DrawingPacket _drawingPacket;
        public DrawingWindow(TcpHandler handler, Client client, dynamic packet)
        {
            _handler = handler;
            _client = client;
            _packet = packet;
            _client.state = packet.type;
            InitializeComponent();
            Background = Brushes.White;
           setDrawingLabel(_packet.prompt);
        }
        private void setDrawingLabel(string p)
        {
            if (DrawingPrompt != null)
            {
                DrawingPrompt.Text = p;
            }
             
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
        private void ImageToFile()
        {
            string resourceFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            if (!System.IO.Directory.Exists(resourceFolder))
            {
                System.IO.Directory.CreateDirectory(resourceFolder);
            }

            string filePath = System.IO.Path.Combine(resourceFolder, "Drawing.jpg");

            CanvasArea.SaveCanvas(filePath);

            // Check the file size and adjust quality if necessary
            int quality = 90;
            FileInfo fileInfo = new FileInfo(filePath);
            while (fileInfo.Length > 1 * 1024 * 1024 && quality > 10) // 1 MB = 1 * 1024 * 1024 bytes
            {
                quality -= 10;
                CanvasArea.SaveCanvas(filePath);
                fileInfo = new FileInfo(filePath);
            }

            MessageBox.Show($"Drawing has been saved Successful. Submitting....");
        }
        //submit button
        private async void SubmitButton_Click(Object sender, RoutedEventArgs e)
        {
            //save the image 
            ImageToFile();
            // Send the drawing to the server
            string resourceFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            string filePath = System.IO.Path.Combine(resourceFolder, "Drawing.jpg");
            //send the image to server
            byte[] data = File.ReadAllBytes(filePath);
            _drawingPacket = new DrawingPacket(data, _client);
            await _handler.SendPacket(_drawingPacket.Serialize());
            //// go to voting screen
            //VotingWindow votingWindow = new VotingWindow(_handler, _client/*, _drawingPacket*/);
            //votingWindow.Show();
            ////close the current window
            //this.Close();

            // go to waiting to vote screen
            VoteWaitingWindow voteWaitingWindow = new VoteWaitingWindow(_handler, _client);
            voteWaitingWindow.Show();
            this.Close();
        }

    }
}

