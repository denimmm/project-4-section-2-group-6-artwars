using ArtWarsClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArtWarsClientWPF.StatePacket
{
    public class WaitingPacket : Packet
    {
        public string type { get; }
        public string roomCode { get; }
        public string prompt { get; set; }
        public int playerId { get; }
        public string jsonString { get; set; } = string.Empty;

        //make new packet from received data
        //when you use this, do not forget to check if type == failed.
        [JsonConstructor]
        public WaitingPacket(string type, string roomCode, string prompt, int playerId)
        {
            this.type = type;
            this.roomCode = roomCode;
            this.prompt = prompt;
            this.playerId = playerId;
        }
        public WaitingPacket(byte[] packet)
        {
            //get the size
            size = BitConverter.ToInt32(packet, 0); //get int from the first 4 bytes
            string json = Encoding.UTF8.GetString(packet, 4, size - 4);

            try
            {

                //make a connectingPacket object and copy its data lol
                var obj = JsonSerializer.Deserialize<WaitingPacket>(json);
                if (obj != null)
                {
                    type = obj.type;
                    roomCode = obj.roomCode;
                    prompt = obj.prompt;
                    playerId = obj.playerId;
                }
                else
                {
                    type = "failed";
                    roomCode = "-1";
                    prompt = "";
                    playerId = -1;
                }
               

            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
                type = "failed";
                roomCode = "-1";
                prompt = "";
                playerId = -1;
            }

            jsonString = json;
            //log received packet to file
            try
            {
                using (StreamWriter writer = new StreamWriter($"{type}LogIn.txt", true))
                {
                    string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - "  + "Incoming: " + size + json;
                    writer.WriteLine(log);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to file: " + e.Message);
            }
        }

        //make new packet to send
        public WaitingPacket(string roomCode, string prompt, string playerId)
        {
            this.type = "prompt";
            this.roomCode = roomCode;
            this.prompt = prompt;
            this.playerId = int.Parse(playerId);

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
            //log sent packet to file
            try
            {
                using (StreamWriter writer = new StreamWriter($"{type}LogOut.txt", true))
                {
                    string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + "Outgoing: " + size + jsonString;
                    writer.WriteLine(log);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to file: " + e.Message);
            }

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
