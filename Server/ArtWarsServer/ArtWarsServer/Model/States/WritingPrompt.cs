using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    class WritingPrompt : State
    {

        private Server server { get; set; }




        public WritingPrompt(Server server)
        {
            this.server = server;
        }


        public async Task Start()
        {



        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new Drawing(server);

        }
    }
}
