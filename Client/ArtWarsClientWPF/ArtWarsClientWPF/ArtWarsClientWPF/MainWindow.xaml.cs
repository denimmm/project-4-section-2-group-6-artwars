using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using ArtWarsClientWPF.Models;
using ArtWarsClientWPF.StatePacket;
using Newtonsoft.Json; // Uses Newtonsoft.Json from the nuget package manager

namespace ArtWarsClientWPF
{
    public partial class MainWindow : Window
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client(PlayerNameBox.Text, RoomCodeBox.Text);
            string Ip = IpCodeBox.Text;
            TcpHandler tcpHandler = new TcpHandler();
            if (string.IsNullOrWhiteSpace(client.player.Name) || string.IsNullOrWhiteSpace(client.roomCode))
            {
                StatusText.Text = "Please enter your name and the room code.";
                return;
            }
            try
            {
                tcpHandler.Connect(Ip, 27000); // Local IP Port, change if needed

                ConnectingPacket connectPacket = new ConnectingPacket(client.roomCode, client.player.Name);
                await tcpHandler.SendPacket(connectPacket.Serialize()); // Await the async call

                StatusText.Text = "Room connection successful!";
                //tcpHandler.ReceivePacket(client); // Await the async call
                byte[] rvBytes = new byte[1024];
                int rvData = await tcpHandler._stream.ReadAsync(rvBytes, 0, rvBytes.Length); // Await the async call
                if (rvData > 0)
                {
                    if (client.player.Id == "-1")
                    {
                        ConnectingPacket rpacket = new ConnectingPacket(rvBytes);
                        client.state = rpacket.type;
                        client.roomCode = rpacket.roomCode;
                        client.player.Name = rpacket.playerName;
                        client.player.Id = rpacket.playerId.ToString();
                    }
                }

                WaitingWindow waitingWindow = new WaitingWindow(tcpHandler, client);
                waitingWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                StatusText.Text = "Connection failed: " + ex.Message;
            }
        }
    }
}
