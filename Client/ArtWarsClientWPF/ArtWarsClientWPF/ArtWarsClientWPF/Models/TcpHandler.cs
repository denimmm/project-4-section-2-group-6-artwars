using ArtWarsClientWPF.StatePacket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using System.Text;

namespace ArtWarsClientWPF.Models
{
    public class TcpHandler
    {
        public TcpClient _client;
        public NetworkStream _stream;
        public void Connect(String hostIP, int port)
        {
            _client = new TcpClient();
            _client.Connect(hostIP, port);
            _stream = _client.GetStream();
        }
        public async Task SendPacket(byte[] packet)
        {
            //string jsonMessage = JsonConvert.SerializeObject(message);
            //byte[] data = Encoding.UTF8.GetBytes(packet);
            _stream.Write(packet, 0, packet.Length);
        }
        public async void ReceivePacket (Client client){
            byte[] data = new byte[1024];
            int bytes = await _stream.ReadAsync(data, 0, data.Length);
            //string jsonDataRv = Encoding.UTF8.GetString(data, 0, bytes);
            //return JsonConvert.DeserializeAnonymousType(jsonDataRv, anyPacket);
            if (bytes > 0)
            {
                if (client.player.Id == "-1")
                {
                    ConnectingPacket rpacket = new ConnectingPacket(data);
                    client.state = rpacket.type;
                    client.roomCode = rpacket.roomCode;
                    client.player.Name = rpacket.playerName;
                    client.player.Id = rpacket.playerId.ToString();
                }
                else { 
                    // decise to go waiting or writing prompt

                }
            }
        }
    }
}
