using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ArtWarsServer.Model
{



    public class Server
    {
        public string code;
        public string IP_Address;

        //list of players in the game
        public List<Player> Players { get; private set; }
        //current state of the game
        public State state { get; set; }
        //last player id
        public int PlayerID_Index { get; set; } //this is used to keep track of the last issued player id

        //prompt for the round
        public string prompt;
        
        //server config settings
        public ServerConfig serverConfig {get; private set;}
        
            public Server()
        {
            //initialize players list
            Players = new List<Player>();

            PlayerID_Index = 0;

            prompt = "PROMPT NOT SET";

            serverConfig = new ServerConfig();

            IP_Address = GetIPAddress();

            //code = MakeRoomCode();
            code = "1234";

            //initialize state
            state = new Connecting(this);

        }

        public void AddPlayer(Player player)
        {
            //assign an id to the new player
            PlayerID_Index++;
            player.ID = PlayerID_Index;

            //add player to the list of players
            Players.Add(player);
        }

        public void RemovePlayer(Player p)
        {
            Players.Remove(p);
        }

        //returns the server's ip address
        private string GetIPAddress()
		{
            string hostName = Dns.GetHostName();

            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            return IP;
		}

        //makes a new room code with a random number
        public string MakeRoomCode()
        {
            Random random = new Random();

            //trim to 4 digits or whatever ROOM_CODE_MAX_NUMBER is
            return (random.Next() % serverConfig.ROOM_CODE_MAX_NUMBER).ToString("D4");
        }

        public bool verifyRoomCode(string roomCode)
        {
            if (this.code == roomCode) return true;

            return false;
        }


        public void Start()
        {
            state.Start();

        }

        public void Stop()
        {



        }
    }
}