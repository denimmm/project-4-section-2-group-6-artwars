using ArtWarsClientWPF.Models;
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
    public partial class PromptWritingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;

        public PromptWritingWindow(TcpHandler handler, Client client)
        {
            _handler = handler;
            _client = client;
            InitializeComponent();

        }

        private async void SubmitPromptButton_Click(object sender, RoutedEventArgs e)
        {
            string prompt = PromptBox.Text;
            if (prompt != null)
            {
                // make a packet from server with the prompt 

                WaitingPacket promptPacket = new WaitingPacket(_client.roomCode, prompt, _client.player.Id);

                await _handler.SendPacket(promptPacket.Serialize());
                PromptWaitingWindow promptWaitingWindow = new PromptWaitingWindow(_handler, _client);
                promptWaitingWindow.Show();
                this.Close();
            }
            else
            {

            }
        }
    }
}
