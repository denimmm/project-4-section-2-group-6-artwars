using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{

	//might not need this at all
	public enum PacketType
	{
		Connecting,
		RequestPrompt,
		SendPrompt,
		SendPic,
		SendAllPics,
		SendVote,
		SendState,

		unknown = 0
	}



	internal class Packet
	{
		public int RoomCode { get; set; }

		public int PlayerID { get; set; }

		public PacketType packetType { get; set; }

		public string Data { get; set; }


		public Packet(string data) { 
			var deserialized = JsonSerializer.Deserialize<Packet>(data);

			if (deserialized != null)
			{
				this.RoomCode = deserialized.RoomCode;

				this.PlayerID = deserialized.PlayerID;

				this.packetType = deserialized.packetType;

				this.Data = deserialized.Data;
			}
			else
			{
				this.RoomCode = -1;
				this.PlayerID = -1;
				this.packetType = PacketType.unknown;
				this.Data = "";


			}
		}


		public byte[] Serialize()
		{
			string json = JsonSerializer.Serialize(this);
			return Encoding.UTF8.GetBytes(json);
		}


	}
}
