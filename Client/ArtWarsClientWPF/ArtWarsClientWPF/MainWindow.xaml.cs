using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
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
            string playerName = PlayerNameBox.Text;
            string roomCode = RoomCodeBox.Text;
            int playerId;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(roomCode))
            {
                StatusText.Text = "Please enter your name and the room code.";
                return;
            }
            try
            {
                _client = new TcpClient("127.0.0.1", 25565); // Local IP Port, change if needed
                _stream = _client.GetStream();

                // Creates the JSON Packet...may need to add IP Address too for connecting to the server
                var connectPacket = new
                {
                    type = "connect",
                    code = roomCode,
                    playerName = playerName,
                    playerId = -1};

                string jsonData = JsonConvert.SerializeObject(connectPacket);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);

                // Sends data to the server
                _stream.Write(data, 0, data.Length);
                StatusText.Text = "Room connection successful!";
            }
            catch (Exception ex)
            {
                StatusText.Text = "Connection failed: " + ex.Message;
            }
        }
    }
}
