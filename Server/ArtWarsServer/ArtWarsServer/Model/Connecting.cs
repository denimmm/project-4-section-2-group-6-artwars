using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    class Connecting : State
    {
        bool running;
        private Server server { get; set; }




        public Connecting(Server server) {
            running = true;
        
            this.server = server;
        }


        public void Start()
        {
            connectPlayers();


        }

        public void NextState()
        {
            running = false;
            //make and assign new state to server
            server.state = new WritingPrompt(server);

        }

        private void connectPlayers()
        {


        }





    }
}
