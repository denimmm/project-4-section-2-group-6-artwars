using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Security.Cryptography.Pkcs;
using System.Diagnostics;

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

            //set number of rounds based on number of players
            server.serverConfig.NumberOfRounds = server.Players.Count;


            Debug.WriteLine("connecting state created");
        }


        public async Task Start()
        {
            await connectPlayers();

            NextState();
            await server.state.Start();
        }

        public void NextState()
        {
            running = false;

            //stop accepting new connections
            tcpListener.Stop();

            //make and assign new state to server
            server.state = new WritingPrompt(server);
            //server.state.Start();
        }

        private async Task connectPlayers()
        {

            //start the tcp listener
            //this will asynchronously make a connection with every player
            tcpListener.Start();
            Debug.WriteLine("waiting for connections");
            //wait for connections, make new thread and player for each.
            while (running)
            {

                try
                {

                    var clientSocket = await tcpListener.AcceptTcpClientAsync();

                    //make new player with the socket only
                    Player newPlayer = new Player(clientSocket, server);

                    //open up an async recv to obtain the player's name and verify with roomcode.
                    _ = Task.Run(async () => await ProcessUser(newPlayer));

                }
                catch(Exception ex){
                    Debug.WriteLine($"Error accepting player: {ex.Message}");

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
                    Debug.WriteLine("Error: Player failed to receive data");
                    newPlayer.Disconnect();
                    return;

                }


                //make packet object
                ConnectingPacket recvPacket = new ConnectingPacket(receivedData);

                if (recvPacket.type == "failed")
                {
                    Debug.WriteLine("Received Packet failed to be assigned to object. disconnecting player");
                    newPlayer.Disconnect();
                    return;
                }

                //verify room code (authentication)
                if (!server.verifyRoomCode(recvPacket.roomCode))
                {
                    //disconnect player
                    newPlayer.Disconnect();
                }

                //assign player's name
                newPlayer.Name = recvPacket.playerName;

                //add the player to server and assign id
                server.AddPlayer(newPlayer);



                ConnectingPacket responsePacket = new ConnectingPacket(server.code, newPlayer.Name, newPlayer.ID);

                //send packet back to player with the new player's id
                await newPlayer.sendDataAsync(responsePacket);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error processing player: {ex.Message}");
                newPlayer.Disconnect();
            }


        }

        public static void Reset()
        {
        }




    }
}
