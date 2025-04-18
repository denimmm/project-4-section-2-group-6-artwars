﻿using ArtWarsClientWPF.Models;
using ArtWarsClientWPF.StatePacket;
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
    public partial class VoteWaitingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;
        public VoteWaitingWindow(TcpHandler handler, Client client)
        {
            _handler = handler;
            _client = client;
            InitializeComponent();
            //start receiving the vote from server
            _ = ReceiveVoteFromServer();
        }
        // to receive the vote from the server
        //one start seding packets for vote proceed to voting
        private async Task ReceiveVoteFromServer()
        {
            byte[] data = new byte[2 * 1024 * 1024];
            int bytes = await _handler._stream.ReadAsync(data, 0, data.Length);
            DrawingPacket drawingPacket = new DrawingPacket(data);
            //proceed to voting
            if (drawingPacket.type != null)
            {
                //    //open the voting window
                VotingWindow votingWindow = new VotingWindow(_handler, _client, drawingPacket);
                votingWindow.Show();
                this.Close();
            }

            else
            {
                //wait for the vote
                _ = ReceiveVoteFromServer();
            }
        }
    }
}
