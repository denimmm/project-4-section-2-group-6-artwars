using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

using System.Linq.Expressions;
using System.IO;

namespace ArtWarsServer.Model
{
    public class Player
    {
        //unique identifier for each player
        public int ID { get; set; }
        public string Name { get; set; }
        public int score { get; set; }

        public TcpClient ClientSocket { get; set; }

		private Stream stream => ClientSocket.GetStream();

        private Server server { get; set; }



        public Player(TcpClient socket, Server server)
        {
            //set player name
            this.Name = "new player";

            //set id
            this.ID = -1;

            //set player socket
            ClientSocket = socket;

            //set the score to 0
            score = 0;
        }


        //sends a packet to a player
        public async Task sendDataAsync(string message)
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    //serialize data
                    byte[] data = Encoding.UTF8.GetBytes(message);

                    //send data
					await stream.WriteAsync(data, 0, data.Length);
                }
                else
                {
                    Console.WriteLine($" {Name}, Player ID: {ID} is not connected");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to player \"{Name}\" ID: {ID}. {ex.Message}");
            }

        }

        public async Task<byte[]> ReceiveDataAsync()
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    byte[] buffer = new byte[server.serverConfig.bufferSize];

                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        return buffer;
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error recieving data from player {ID} {Name} : {ex.Message}");
            }

            return null;

        }



    }
}
