using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace ArtWarsClientWPF.Models
{
    public class Player
    {
        public string Name;
        public string Id;
        public int score;
        public Player() { 
            Name = string.Empty;
            Id = "-1";
            score = 0;
        }
        public void setPlayerName(string planyerName)
        {
            Name = planyerName;
        }
        public void setScore(int playerScore)
        {
            score = playerScore;
        }
        public void setPlayerId(string playerId)
        {
            Id = playerId;
        }
        public string getPlanyerName()
        {
            return Name;
        }
        public int getScore()
        {
            return score;
        }
        public string getPlayerId()
        {
            return Id;
        }
    }
}
