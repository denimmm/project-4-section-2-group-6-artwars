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
        }

        //wait for the server to broadcast the prompt
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            byte[] rvdata = new byte[1024];
            int inbytes = await _handler._stream.ReadAsync(rvdata, 0, rvdata.Length);
            //once received, put in waiting packet and pass it to drawing window
            if (inbytes > 0)
            {
                WaitingPacket waiting = new WaitingPacket(rvdata);
                DrawingWindow drawingWindow = new DrawingWindow(_handler, _client, waiting);
                drawingWindow.Show();
                this.Close();
            }
        }
    }
}
