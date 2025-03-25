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
        private TcpClient _client;
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
        public async Task <T> ReceivePacket <T>(T anyPacket){
            byte[] data = new byte[1024];
            int bytes = _stream.Read(data, 0, data.Length);
            string jsonDataRv = Encoding.UTF8.GetString(data, 0, bytes);
            return JsonConvert.DeserializeAnonymousType(jsonDataRv, anyPacket);
            
        }
        public void Close()
        {
            _stream.Close();
            _client.Close();
        }
    }
}
