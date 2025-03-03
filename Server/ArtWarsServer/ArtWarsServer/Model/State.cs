using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    interface State
    {

        //begin the state
        void Start();

        //change to next state. parameter tells the state who the server is.
        void NextState();
    }
}
