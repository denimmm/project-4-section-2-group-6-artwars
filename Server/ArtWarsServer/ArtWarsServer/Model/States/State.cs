using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    public interface State
    {

        //begin the state
        Task Start();

        //change to next state. parameter tells the state who the server is.
        void NextState();

        abstract static void Reset();
    }
}
