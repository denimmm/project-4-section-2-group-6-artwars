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
        public int ID { get;}
        public string Name { get; }
        public int score { get; set; }

        private Socket _socket;




        Player(string name, int id, Socket socket)
        {
            //set player name
            this.Name = name;

            //set id
            this.ID = id;

            //set player socket
            _socket = socket;

            //set the score to 0
            score = 0;
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
