using ArtWarsServer.Model.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;    

namespace ArtWarsServer.Model
{
    class Voting : State
    {
        Server server;

        public Voting(Server server)
        {
            this.server = server;
            server.MainFrame?.Navigate(new View.VotingPage());
        }


        public async Task Start()
        {
            string state = "Drawing";
            for (int i = 0; i < server.Players.Count; i++)
            {
                if (i == server.Players.Count)
                {
                    state = "Voting";
                }
                DrawingPacket drawingPacket = new DrawingPacket(state, server.code, convertImageToBytes(server.Players[i].ID),server.Players[i].ID.ToString());
                await server.BroadcastToPlayers(drawingPacket);
            }







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

        private async Task SendAllDrawings(Player player)
        {

        }


        public void NextState()
        {
            //make and assign new state to server
            server.state = new Results(server);
        }

        public static void Reset()
        {
        }

    }
}
