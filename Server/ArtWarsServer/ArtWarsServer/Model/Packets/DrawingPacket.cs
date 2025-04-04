using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtWarsServer.Model.Packets
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

        public DrawingPacket(byte[] packet)
        {
            size = BitConverter.ToInt32(packet, 0);
            string json = Encoding.UTF8.GetString(packet, 4, size - 4);

            try
            {
                var obj = JsonSerializer.Deserialize<DrawingPacket>(json);
                if (obj != null)
                {
                    type = obj.type;
                    roomCode = obj.roomCode;
                    image = Convert.FromBase64String(obj.jsonString); // Convert from Base64
                    playerId = obj.playerId;
                }
                else
                {
                    throw new Exception("Failed to deserialize packet.");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
        }
        public override byte[] Serialize()
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            byte[] serializedData = new byte[jsonBytes.Length + HEADER_SIZE];

            Buffer.BlockCopy(BitConverter.GetBytes(serializedData.Length), 0, serializedData, 0, HEADER_SIZE);
            Buffer.BlockCopy(jsonBytes, 0, serializedData, HEADER_SIZE, jsonBytes.Length);

            return serializedData;
        }
    }
}
