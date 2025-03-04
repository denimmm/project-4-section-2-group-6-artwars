using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ArtWarsServer.Model
{
    class Connecting : State
    {
        bool running;
        private Server server { get; set; }

        private TcpListener tcpListener;
        


        public Connecting(Server server) {
            running = true;
        
            this.server = server;

            //start listening for connections
            tcpListener = new TcpListener(IPAddress.Any, server.serverConfig.Port);

        }


        public void Start()
        {
            connectPlayers();

            NextState();
        }

        public void NextState()
        {
            running = false;
            //make and assign new state to server
            server.state = new WritingPrompt(server);

        }

        private async void connectPlayers()
        {

            //start the tcp listener
            tcpListener.Start();

            //create server socket



            //wait for connections, make new thread for each.
            while (running)
            {
                var clientSocket = await tcpListener.AcceptTcpClientAsync();

                server.AddPlayer(new Player(clientSocket));

                
            }

        }





    }
}
