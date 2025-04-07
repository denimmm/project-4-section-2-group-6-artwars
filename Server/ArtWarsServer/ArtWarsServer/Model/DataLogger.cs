using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ArtWarsServer.Model
{

    class DataLogger
    {
        bool writing;
        ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();


    }

}
