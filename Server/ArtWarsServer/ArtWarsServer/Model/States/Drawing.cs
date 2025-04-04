using ArtWarsServer.Model.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArtWarsServer.Model
{
    class Drawing : State
    {

        Server server;

        public Drawing(Server server)
        {
            this.server = server;
        }


        public async Task Start()
        {
            try
            {
                // Ensure the Images directory exists
                Directory.CreateDirectory(server.serverConfig.ImageFolder);

                // Create tasks to receive and save drawings from all players
                var tasks = server.Players.Select(player => ReceiveAndSaveDrawingAsync(player)).ToList();

                // Wait for all tasks to complete
                await Task.WhenAll(tasks);

                // Proceed to next state
                NextState();
                await server.state.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Drawing stage: {ex.Message}");
                // Handle the error as needed
            }
        }

        private async Task ReceiveAndSaveDrawingAsync(Player player)
        {
            try
            {
                // Receive the packet data from the player
                byte[] data = await player.ReceiveDataAsync();
                if (data == null || data.Length == 0)
                {
                    Debug.WriteLine($"No data received from player {player.ID}");
                    return;
                }

                // Deserialize the packet
                DrawingPacket packet = new DrawingPacket(data);

                // Check if the image data is valid
                if (packet.image == null || packet.image.Length == 0)
                {
                    Debug.WriteLine($"Invalid image data from player {player.ID}");
                    return;
                }

                // Construct the image path with player ID and current round
                string imagePath = Path.Combine(
                    server.serverConfig.ImageFolder,
                    $"{player.ID}.jpg"
                );

                // Save the image
                File.WriteAllBytes(imagePath, packet.image);
                Debug.WriteLine($"Saved image for player {player.ID} at {imagePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing player {player.ID}'s drawing: {ex.Message}");
            }
        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new Voting(server);
        }


        public static void Reset()
        {
        }


    }
}
