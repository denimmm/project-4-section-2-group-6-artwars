using ArtWarsClientWPF.Models;
using Newtonsoft.Json;
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
    /// Interaction logic for WaitingWindow.xaml
    /// </summary>
    public partial class WaitingWindow : Window
    {
        private TcpHandler tcpHandler;
        private Client client;
        public WaitingWindow(TcpHandler handler, Client client)
        {
            tcpHandler = handler;
            client = client;
            InitializeComponent(); 
             async Task ReceivePacketsAsync()
        {
            while (true)
            {
                var connectPacket = new
                {
                    type = "",
                    code = "",
                    playerName = "",
                    playerId = ""
                };

                byte[] rvBytes = new byte[1024];
                int rvData = tcpHandler._stream.Read(rvBytes, 0, rvBytes.Length);
                if (rvData > 0)
                {
                    if (client.player.Id == "-1")
                    {

                        string jsonDataRv = Encoding.UTF8.GetString(rvBytes, 0, rvData);
                        var dataReceived = JsonConvert.DeserializeAnonymousType(jsonDataRv, connectPacket);

                        client.state = dataReceived.type;
                        client.roomCode = dataReceived.code;
                        client.player.Name = dataReceived.playerName;
                        client.player.Id = dataReceived.playerId.ToString();
                    }
                    else
                    {
                        //byte[] rvBytes = new byte[1024];
                        rvData = tcpHandler._stream.Read(rvBytes, 0, rvBytes.Length);
                        string jsonDataRv = Encoding.UTF8.GetString(rvBytes);
                        var dataReceived = JsonConvert.DeserializeAnonymousType(jsonDataRv, connectPacket);

                        if (dataReceived.ToString() == client.player.Id)
                        {
                            PromptWritingWindow promptWritingWindow = new PromptWritingWindow(tcpHandler, client);
                            promptWritingWindow.Show();
                            //this.Close();
                        }
                        else
                        {
                            PromptWaitingWindow promptWaitingWindow = new PromptWaitingWindow(tcpHandler, client);
                            promptWaitingWindow.Show();
                            this.Close();
                        }
                    }
                }

            }
        }
        }
       
    }
}
