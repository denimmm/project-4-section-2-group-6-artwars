using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    abstract class Packet
    {
        public const int HEADER_SIZE = 4;

        public int size;

        public abstract byte[] Serialize();
    }
}
