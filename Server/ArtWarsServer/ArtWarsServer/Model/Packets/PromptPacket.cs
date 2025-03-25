using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


using System.Text.Json;

namespace ArtWarsServer.Model
{


    class PromptPacket : Packet
    {
        public string type { get; }
        public string roomCode { get; }
        public string prompt { get; set; }
        public int playerId { get; }

        public string jsonString;

        //make new packet from received data
        //when you use this, do not forget to check if type == failed.
        public PromptPacket(byte[] packet)
        {
            //get the size
            size = BitConverter.ToInt32(packet, 0); //get int from the first 4 bytes
            string json = Encoding.UTF8.GetString(packet, 4, size -4);

            try
            {

                //make a connectingPacket object and copy its data lol
                var obj = JsonSerializer.Deserialize<PromptPacket>(json);
                if (obj != null)
                {
                    type = obj.type;
                    roomCode = obj.roomCode;
                    prompt = obj.prompt;
                    playerId = obj.playerId;
                }

            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
                type = "failed";
                roomCode = "-1";
                prompt = "";
                playerId = -1 ;
            }


        }

        //make new packet to send
        public PromptPacket(string roomCode, string prompt, int playerId) {
            this.type = "prompt";
            this.roomCode = roomCode;
            this.prompt = prompt;
            this.playerId = playerId;

            var json = new
            {
                type = this.type,
                roomCode = this.roomCode,
                prompt = this.prompt,
                playerId = this.playerId
            };

            //serialize the json into a string
            jsonString = JsonSerializer.Serialize(json);

            //set size of packet
            size = HEADER_SIZE + Encoding.UTF8.GetBytes(jsonString).Length;

        }


        public override byte[] Serialize()
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
