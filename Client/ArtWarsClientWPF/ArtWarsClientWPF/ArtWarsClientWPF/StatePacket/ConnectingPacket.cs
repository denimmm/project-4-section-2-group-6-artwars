using ArtWarsClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Linq.Expressions;

namespace ArtWarsClientWPF.StatePacket
{
    public class ConnectingPacket : Packet
    {
        public string type;
        public string roomCode;
        public string playerName;
        public int playerId;
        public string jsonString;



        //used for json
        [JsonConstructor]
        public ConnectingPacket(string type, string roomCode, string playerName, int playerId)
        {
            this.type = type;
            this.roomCode = roomCode;
            this.playerName = playerName;
            this.playerId = playerId;
        }

        //make a packet from upcoming packet with series of bytes
        public ConnectingPacket(byte[] bytes)
        {
            //get the size from bytes 
            size = BitConverter.ToInt32(bytes, 0);
            string json = Encoding.UTF8.GetString(bytes, HEADER_SIZE, size - HEADER_SIZE);
            try
            {
                var packet = JsonConvert.DeserializeObject<ConnectingPacket>(json);
                type = packet.type;
                roomCode = packet.roomCode;
                playerName = packet.playerName;
                playerId = packet.playerId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //log received packet to file
            try
            {
                using(StreamWriter writer = new StreamWriter("ConnectingLongIn.txt", false))
                {
                    string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + "Incoming: "+size + json;
                    writer.WriteLine(log);

                }

            }
            catch
            {
                Console.WriteLine("Error writing to file");
            }
        }
        //make a packet from log in screen
        public ConnectingPacket(string roomCode, string playerName)
        {
            this.type = "connecting";
            this.roomCode = roomCode;
            this.playerName = playerName;
            this.playerId = -1;

            var json = new
            {
                type = this.type,
                roomCode = this.roomCode,
                playerName = this.playerName,
                playerId = this.playerId
            };
            //make a json string
            jsonString = JsonConvert.SerializeObject(json);

            size = HEADER_SIZE + jsonString.Length;
            //log sent packet to file
            try
            {
                using (StreamWriter writer = new StreamWriter("ConnectingLongOut.txt", false))
                {
                    string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + "Outgoing: "+ size + jsonString;
                    writer.WriteLine(log);
                }

            }
            catch
            {
                Console.WriteLine("Error writing to file");
            }
        }
        //make a string from a packet
        public override string ToString()
        {
            var json = new
            {
                type = this.type,
                roomCode = this.roomCode,
                playerName = this.playerName,
                playerId = this.playerId
            };
            return JsonConvert.SerializeObject(json);
        }
        public byte[] Serialize()
        {

            byte[] serializedData = new byte[size];

            //set the size in the new byte packet
            Buffer.BlockCopy(BitConverter.GetBytes(size), 0, serializedData, 0, HEADER_SIZE);

            //add the json to the packet
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            Buffer.BlockCopy(jsonBytes, 0, serializedData, HEADER_SIZE, jsonBytes.Length);

            return serializedData;
        }

    }

}

