using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Security.Cryptography.Pkcs;

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
            Console.WriteLine("connecting state created");
        }


        public async Task Start()
        {
            await connectPlayers();

            NextState();
        }

        public void NextState()
        {
            running = false;

            //stop accepting new connections
            tcpListener.Stop();

            //make and assign new state to server
            server.state = new WritingPrompt(server);

        }

        private async Task connectPlayers()
        {

            //start the tcp listener
            //this will asynchronously make a connection with every player
            tcpListener.Start();
            Console.WriteLine("waiting for connections");
            //wait for connections, make new thread and player for each.
            while (running)
            {

                try
                {

                    var clientSocket = await tcpListener.AcceptTcpClientAsync();

                    //make new player with the socket only
                    Player newPlayer = new Player(clientSocket, server);

                    //this gives the player an id and adds it to our list
                    server.AddPlayer(newPlayer);


                    //open up an async recv to obtain the player's name and verify with roomcode.
                    _ = Task.Run(async () => await ProcessUser(newPlayer));

                }
                catch(Exception ex){
                    Console.WriteLine($"Error accepting player: {ex.Message}");

                }

                
            }

            //NOTE: new connections are stopped in the nextState() function because it is better this way
        }

        //assigns the Player's name and verifies their room code
        private async Task ProcessUser(Player newPlayer)
        {

            try
            {

                //get the packet
                string receivedData = await newPlayer.ReceiveDataAsync();
                if (string.IsNullOrEmpty(receivedData))
                {
                    Console.WriteLine("Error: Player faield to send data");
                    //newPlayer.Disconnect();
                    return;

                }



                //get the json from the packet

                //verfy room code

                //add the id to the player

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error processing player: {ex.Message}");
            }


        }





    }
}
