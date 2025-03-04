using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    class Server
    {
        //list of players in the game
        public List<Player> Players { get; set; }
        public State state { get; set; }
        public int PlayerID_Index { get; set; } //this is used to keep track of the last issued player id

        public string prompt;
        //////server settings//////
        
        public Dictionary<string, int> Settings = new Dictionary<string, int>();
        
            Server()
        {
            //initialize players list
            Players = new List<Player>();

            PlayerID_Index = 0;

            //initialize state
            state = new Connecting(this);

            prompt = "PROMPT NOT SET";
        }

        /// <summary>
        /// assigns an id to a player object and adds it to the list
        /// </summary>
        /// <param name="p"></param>
        void AddPlayer(Player p)
        {

        }

        void RemovePlayer(Player p)
        {
            Players.Remove(p);
        }

    }
}