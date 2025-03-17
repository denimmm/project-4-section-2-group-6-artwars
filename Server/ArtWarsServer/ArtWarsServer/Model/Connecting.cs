using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Security.Cryptography.Pkcs;

using System.Text.Json;

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
                byte[] receivedData = await newPlayer.ReceiveDataAsync();

                //check if empty
                if (receivedData == null || receivedData.Length <= 4)
                {
                    Console.WriteLine("Error: Player faild to receive data");
                    //newPlayer.Disconnect();
                    return;

                }


                //make packet object
                ConnectingPacket recvPacket = new ConnectingPacket(receivedData);


                //verify room code
                if (!server.verifyRoomCode(recvPacket.roomCode))
                {
                    //disconnect player
                }

                //assign player's name
                newPlayer.Name = recvPacket.playerName;

                //add the player to server and assign id
                server.AddPlayer(newPlayer);



                //make response packet
                var response = new
                {
                    type = "connect",
                    code = server.code,
                    playerName = newPlayer.Name,
                    playerId = newPlayer.ID
                };

                string responsePacket = JsonSerializer.Serialize(response);
                //send packet back to player with the new player's id
                await newPlayer.sendDataAsync(responsePacket);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error processing player: {ex.Message}");
            }


        }





    }
}
