using System;
using System.Collections.Generic;
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


        private int PlayerID_Index;//this is used to keep track of the last issued player id

        
            Server()
        {
            //initialize players list
            Players = new List<Player>();
            PlayerID_Index = 0;
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