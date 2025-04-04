using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArtWarsServer.Model
{
    class Voting : State
    {
        Server server;

        public Voting(Server server)
        {
            this.server = server;
            server.MainFrame?.Navigate(new View.VotingPage());
        }


        public async Task Start()
        {



        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new Results(server);
        }

        public static void Reset()
        {
        }

    }
}
