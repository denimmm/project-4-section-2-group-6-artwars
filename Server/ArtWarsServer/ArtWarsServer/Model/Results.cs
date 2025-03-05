using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArtWarsServer.Model
{
    class Results : State
    {
        Server server;

        public Results(Server server)
        {
            this.server = server;
        }


        public void Start()
        {



        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new WritingPrompt(server);

        }
    }
}
