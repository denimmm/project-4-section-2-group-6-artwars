using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArtWarsClientWPF.Models
{
    public class Client
    {
        public Player player = new Player();
        public string state;
        public string roomCode;
        public Client(string playerName, string RoomCode) {
         
            player.Id = "-1";
            player.Name = playerName;
            state = "";
            roomCode = RoomCode;
        }
        
        public void setState(string State)
        {
            state = State;
        }
        public string getState()
        {
            return state;
        }
        public Player getPlayer()
        {
            return player;
        }
    }
}
