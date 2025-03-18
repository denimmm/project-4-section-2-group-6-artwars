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


        public async Task Start()
        {



        }

        public void NextState()
        {



            //if there are more rounds left, restart at WritingPrompt
            if(server.serverConfig.NumberOfRounds > server.CurrentRound)
            {


            }

            //make and assign new state to server
            server.state = new WritingPrompt(server);

        }

        public static void Reset()
        {
        }
    }
}
