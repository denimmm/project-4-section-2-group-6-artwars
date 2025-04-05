using ArtWarsClientWPF.Models;
using ArtWarsClientWPF.StatePacket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private byte[] rvdata = new byte[1024];

        public WaitingWindow(TcpHandler handler, Client client)
        {
            tcpHandler = handler;
            this.client = client;
            InitializeComponent();
            _ = InitializeData(); 
        }

        private async Task InitializeData()
        {
            int inbytes = await tcpHandler._stream.ReadAsync(rvdata,0, rvdata.Length);
            while (true)
            {
                if (inbytes > 0)
                {
                    WaitingPacket waiting = new WaitingPacket(rvdata);
                    // if playerid == waiting.playerid
                    if (client.player.Id == waiting.playerId.ToString())
                    {
                        client.state = waiting.type;
                        // go to prompt writing window
                        PromptWritingWindow promptWritingWindow = new PromptWritingWindow(tcpHandler, client);
                        promptWritingWindow.Show();
                        this.Close();
                        break;
                    }
                    else
                    {
                        // go to prompt window
                        client.state = waiting.type;
                        PromptWaitingWindow promptWaitingWindow = new PromptWaitingWindow(tcpHandler, client);
                        promptWaitingWindow.Show();
                        this.Close();
                        break;
                    }
                }
            
           
            }
        }
        
    }
    

}

