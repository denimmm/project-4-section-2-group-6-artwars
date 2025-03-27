using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ArtWarsServer.Model
{
    class Clock
    {
        bool status;
        System.Timers.Timer timer;

        Clock(int time){
            status = true;
            timer = new System.Timers.Timer(time);





       }

    }
}
