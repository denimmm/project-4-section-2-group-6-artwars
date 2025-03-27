using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArtWarsServer.Model
{
    class Drawing : State
    {

        Server server;

        public Drawing(Server server)
        {
            this.server = server;
        }


        public async Task Start()
        {
            //make a list containing all of the tasks
            List<Task<bool>> Tasks = new List<Task<bool>>();


            //wait for users to submit prompts


            //wait for timer

            //cancel recv when time runs out


            //go to next state
            NextState();

        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new Voting(server);
        }


        public static void Reset()
        {
        }


    }
}
