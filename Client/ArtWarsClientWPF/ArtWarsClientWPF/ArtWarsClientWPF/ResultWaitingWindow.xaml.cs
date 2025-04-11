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
    /// <summary>
    /// Interaction logic for ResultWaitingWindow.xaml
    /// </summary>
    public partial class ResultWaitingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;
        public ResultWaitingWindow(TcpHandler handler, Client client)
        {
            _handler = handler;
            _client = client;
            InitializeComponent();
            _ = ReceiveResultFromServer();
        }
        //wait for the result from the server
        private async Task ReceiveResultFromServer()
        {
            byte[] data = new byte[2 * 1024 * 1024];
            int bytes = await _handler._stream.ReadAsync(data, 0, data.Length);
            DrawingPacket drawingPacket = new DrawingPacket(data);
            //proceed to voting
            if (drawingPacket.type != null)
            {
                //    //open the voting window
                ResultWindow resultWindow = new ResultWindow(_handler, _client, drawingPacket);
                resultWindow.Show();
                this.Close();
            }
            else
            {
                //wait for the vote
                _ = ReceiveResultFromServer();
            }
        }
    }
}
