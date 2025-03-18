using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    public class ServerConfig
    {
        //server settings//
        public int Port;
        public int Min_Players;
        public int Max_Players;
        public int Voting_Time;
        public int Drawing_Time;
        public int Prompt_Writing_Time;
        public int Results_Time;
        public int ROOM_CODE_MAX_NUMBER;
        public int bufferSize;
        public int NumberOfRounds;

		public ServerConfig()
        {
            Port = 25565;
            Max_Players = 4;
            Min_Players = 3;
            Prompt_Writing_Time = 60;
            Voting_Time = 60;
            Drawing_Time = 360;
            Results_Time = 30;
			ROOM_CODE_MAX_NUMBER = 10000;
            bufferSize = 4096;
            NumberOfRounds = 0;
		}

    }
}
