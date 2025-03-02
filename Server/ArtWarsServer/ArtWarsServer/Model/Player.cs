using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

using System.Linq.Expressions;

namespace ArtWarsServer.Model
{
    class Player
    {
        //unique identifier for each player
        public int ID { get; set; }

        public string Name { get; }

        private Socket _socket;



        Player(string name, Socket socket)
        {
            //set player name
            this.Name = name;

            //set player socket
            _socket = socket;
        }


        public async Task sendData(string message, int Size)
        {
            try
            {
                if (_socket.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await _socket.SendAsync(data, SocketFlags.None);
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
    }
}
