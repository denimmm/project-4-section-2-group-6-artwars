using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            //wait 10 seconds then go to next round
            await Task.Delay(server.serverConfig.Results_Time);
            this.NextState();

        }

        public void NextState()
        {



            //if there are more rounds left, restart at WritingPrompt
            if(server.serverConfig.NumberOfRounds > server.CurrentRound)
            {
                //make and assign new state to server
                server.state = new WritingPrompt(server);
                server.MainFrame?.Navigate(new View.WritingPromptPage());
                server.state.Start();
            }

        }

        public static void Reset()
        {
        }
    }
}
