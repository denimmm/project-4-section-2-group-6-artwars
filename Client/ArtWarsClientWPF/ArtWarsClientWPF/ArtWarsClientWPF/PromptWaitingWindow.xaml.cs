using ArtWarsClientWPF.Models;
using ArtWarsClientWPF.StatePacket;
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
            _ = ReceivePacketAsysc();
        }

        private async Task ReceivePacketAsysc()
        {
            byte[] bytes = new byte[1024];
            int bytesRec = await _handler._stream.ReadAsync(bytes, 0, bytes.Length);
            if (bytesRec > 0)
            {
                WaitingPacket packet = new WaitingPacket(bytes);
                //change type to propmpt
                _client.state = packet.type;
                //got to drawing window
                if (packet.prompt != null)
                {

                    DrawingWindow drawingWindow = new DrawingWindow(_handler, _client, packet);
                    drawingWindow.Show();
                    this.Close();
                }
                else
                {
                    //go to waiting window

                }
            }

        }
    }
}
