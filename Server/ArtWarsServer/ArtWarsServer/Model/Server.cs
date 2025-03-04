using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ArtWarsServer.Model
{
    class Server
    {
        public int code;
        public string IP_Address;

        //list of players in the game
        public List<Player> Players { get; set; }
        //current state of the game
        public State state { get; set; }
        //last player id
        public int PlayerID_Index { get; set; } //this is used to keep track of the last issued player id

        //prompt for the round
        public string prompt;
        
        //server config settings
        public ServerConfig serverConfig {get; private set;}
        
            Server()
        {
            //initialize players list
            Players = new List<Player>();

            PlayerID_Index = 0;

            //initialize state
            state = new Connecting(this);

            prompt = "PROMPT NOT SET";

            serverConfig = new ServerConfig();

            IP_Address = GetIPAddress();

            code = MakeRoomCode();


        }

        void AddPlayer(Player p)
        {

        }

        void RemovePlayer(Player p)
        {
            Players.Remove(p);
        }


        private string GetIPAddress()
		{
            string hostName = Dns.GetHostName();

            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            return IP;
		}

        private int MakeRoomCode()
        {
            Random random = new Random();

            return random.Next();
        }

    }
}