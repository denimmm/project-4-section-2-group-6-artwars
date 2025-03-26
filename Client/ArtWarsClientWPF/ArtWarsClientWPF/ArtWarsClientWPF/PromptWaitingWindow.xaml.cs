using ArtWarsClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for PromptWaitingWindow.xaml
    /// </summary>
    public partial class PromptWaitingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;
       

        public PromptWaitingWindow(TcpHandler tcp, Client client)
        {
            _handler = tcp;
            _client = client;

            InitializeComponent();
        }
        async Task ReceivePacketAsync() 
        {
            var PromptP = new
            {
                type = "",
                id = "",
                prompt = ""
            };
            //var promptPacket = _handler.ReceivePacket(_client);
            //if(promptPacket != null)
            //{
            //    DrawingWindow drawingWindow = new DrawingWindow(_handler, _client, promptPacket);
            //    drawingWindow.Show();
            //    this.Close();

            //}

        }
    }
}
