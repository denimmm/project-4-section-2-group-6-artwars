using ArtWarsServer.Model;
using System.Net.Sockets;

namespace UnitTests
{
    [TestClass]
    public sealed class PlayerFunctions
    {
        //test that the server makes a room code
        [TestMethod]
        public void player_disconnect_from_server()
        {
            //arrange
            Server server = new Server();//make server
            TcpClient client = new TcpClient();
            Player p = new Player(client, server);//make player
            server.AddPlayer(p);//add player to server

            int num_of_players = server.Players.Count;//get how many players in game

            //act
            server.RemovePlayer(p);
            int new_num_of_players = server.Players.Count;


            //assert
            Assert.IsTrue(new_num_of_players == num_of_players - 1); //player count should go down by 1
        }


    }
}
