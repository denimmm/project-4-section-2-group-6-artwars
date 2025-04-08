using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ArtWarsClientWPF.Models;
namespace ArtWarsClientWPF.StatePacket
{
    public class DrawingPacket : Packet
    {
        public string type { get; }
        public string roomCode { get; }
        public byte[] image { get; set; }
        public string playerId { get; }
        public string jsonString { get; set; } = string.Empty;

        [JsonConstructor]
        public DrawingPacket(string type, string roomCode, byte[] image, string playerId)
        {
            this.type = type;
            this.roomCode = roomCode;
            this.image = image;
            this.playerId = playerId;
        }
        //make a packet to send
        public DrawingPacket(byte[] data, Client client)
        {
            //get the size

            type = "drawing";
            roomCode = client.roomCode;
            image = data;
            playerId = client.player.Id;

            var json = new {

                type = type,
                roomCode = roomCode,
                image = image,
                playerId = playerId
            };
            jsonString = JsonSerializer.Serialize(json);

            size = Encoding.UTF8.GetByteCount(jsonString) + HEADER_SIZE;
            //log sent packet to file
            try
            {
                using (StreamWriter writer = new StreamWriter($"{type}Out.txt", false))
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
        //make new packet from received data
        public DrawingPacket(byte[] packet)
        {
            //get the size
            size = BitConverter.ToInt32(packet, 0); //get int from the first 4 bytes
            string json = Encoding.UTF8.GetString(packet, 4, size - 4);
            try
            {
                //make a connectingPacket object and copy its data lol
                var obj = JsonSerializer.Deserialize<DrawingPacket>(json);
                if (obj != null)
                {
                    type = obj.type;
                    roomCode = obj.roomCode;
                    image = obj.image;
                    playerId = obj.playerId;
                }
                else
                {
                    type = "failed";
                    roomCode = "-1";
                    image = null;
                    playerId = "-1";
                }

            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            //log received packet to file
            try
            {
                // make file name from type

                using (StreamWriter writer = new StreamWriter($"{type}In.txt", false))
                {
                    string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + "Incoming: " + size + json;
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

   
