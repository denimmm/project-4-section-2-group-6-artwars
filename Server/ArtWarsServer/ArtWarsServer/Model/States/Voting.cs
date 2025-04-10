using ArtWarsServer.Model.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace ArtWarsServer.Model
{
    class Voting : State
    {
        Server server;

        List<int> Votes;

        public Voting(Server server)
        {
            this.server = server;
            server.MainFrame?.Navigate(new View.VotingPage());
        }

        //list of ids from player votes.
        public static List<int> votes = new List<int>();

        public async Task Start()
        {

            //send images to players.
            string state = "Drawing";
            for (int i = 0; i < server.Players.Count; i++)
            {
                if (i+1 == server.Players.Count)
                {
                    //change packet type to voting for the last image to tell client to stop receiving
                    state = "Voting";
                }
                DrawingPacket drawingPacket = new DrawingPacket(state, server.code, convertImageToBytes(server.Players[i].ID),server.Players[i].ID.ToString());
                await server.BroadcastToPlayers(drawingPacket);
            }



            //recieve votes
            await RecieveVotesAsync();

            //determine winner
            determineWinner();


            //send winner to players
            await broadcastWinner();

            //next state
            NextState();
        }

        public byte[] convertImageToBytes(int playerId)
        {
            // Send the drawing to the server
            string resourceFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            string filePath = System.IO.Path.Combine(resourceFolder, $"{playerId}.jpg");
            //send the image to server
            byte[] imageBytes = File.ReadAllBytes(filePath);
            return imageBytes;
        }

        //sets votes array and receives all the votes
        private async Task RecieveVotesAsync()
        {
            //list of tasks returning bytestrings
            var voteTasks = new List<Task<byte[]>>();

            //loop through all players
            foreach (Player player in server.Players)
            {
                //make a new task to recieve from each player
                voteTasks.Add(player.ReceiveDataAsync()); 

            }


            List<byte[]> votes = (await Task.WhenAll(voteTasks)).ToList();

            //convert byte[] to int
            Votes = new List<int>();
            foreach (byte[] vote in votes)
            {
                //deserialize the packet
                VotingPacket packet = new VotingPacket(vote);
                //add the vote to the list
                Votes.Add(int.Parse(packet.vote));
            }
        }

        void determineWinner()
        {
            //find mode of the list
            var mode = Votes
                .GroupBy(n => n)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;

            //set the winner
            server.winner = mode;
        }

        async Task broadcastWinner()
        {
            var sendTasks = new List<Task>();

            foreach (Player player in server.Players)
            {
                DrawingPacket drawingPacket = new DrawingPacket("results", server.code, convertImageToBytes(server.winner), server.winner.ToString());

                sendTasks.Add(player.sendDataAsync(drawingPacket));
            }

            await Task.WhenAll(sendTasks);

        }



        public void NextState()
        {
            //make and assign new state to server
            server.state = new Results(server);
            server.MainFrame?.Navigate(new View.ResultsPage());
        }

        public static void Reset()
        {
        }

    }
}
