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

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            //string playerName = PlayerNameBox.Text;
            //string roomCode = RoomCodeBox.Text;
            //int playerId;
            Client client = new Client(PlayerNameBox.Text, RoomCodeBox.Text);
            TcpHandler tcpHandler = new TcpHandler();
            if (string.IsNullOrWhiteSpace(client.player.Name) || string.IsNullOrWhiteSpace(client.roomCode))
            {
                StatusText.Text = "Please enter your name and the room code.";
                return;
            }
            try
            {
                tcpHandler.Connect("127.0.0.1", 27000); // Local IP Port, change if needed
                                                        //_stream = _client.GetStream();

                // Creates the JSON Packet...may need to add IP Address too for connecting to the server
                // After testing, IP Address was not needed but needed to change port
                ConnectingPacket connectPacket = new ConnectingPacket(client.roomCode, client.player.Name);
                //string jsonData = JsonConvert.SerializeObject(connectPacket);
                //byte[] data = Encoding.UTF8.GetBytes(jsonData);


                tcpHandler.SendPacket(connectPacket.Serialize());
                // Sends data to the server
                //_stream.Write(data, 0, data.Length);
                StatusText.Text = "Room connection successful!";
                // Receives data from the server
                
                //wait for server to send the packet with player id

                //async Task ReceivePacketsAsync()
                //{

                    byte[] rvBytes = new byte[1024];
                //
                    int rvData = tcpHandler._stream.Read(rvBytes, 0, rvBytes.Length);
                    if (rvData > 0)
                    {
                        if (client.player.Id == "-1")
                        {

                            string jsonDataRv = Encoding.UTF8.GetString(rvBytes, 0, rvData);
                            var dataReceived = JsonConvert.DeserializeAnonymousType(jsonDataRv, connectPacket);

                            client.state = dataReceived.type;
                            client.roomCode = dataReceived.roomCode;
                            client.player.Name = dataReceived.playerName;
                            client.player.Id = dataReceived.playerId.ToString();
                        }
                    }
                    else
                    {


                    }
               // }
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
