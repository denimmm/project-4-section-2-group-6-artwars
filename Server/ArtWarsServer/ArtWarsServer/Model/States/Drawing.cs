﻿using System;
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
