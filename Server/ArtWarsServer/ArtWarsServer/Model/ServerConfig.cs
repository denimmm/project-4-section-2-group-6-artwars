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
        public string ImageFolder;

		public ServerConfig()
        {
            Port = 27000;
            Max_Players = 4;
            Min_Players = 3;
            Prompt_Writing_Time = 60;
            Voting_Time = 60;
            Drawing_Time = 360;
            Results_Time = 10;
			ROOM_CODE_MAX_NUMBER = 10000;
            bufferSize = 2000000;
            NumberOfRounds = 0;
            ImageFolder = "Images";
		}

    }
}
