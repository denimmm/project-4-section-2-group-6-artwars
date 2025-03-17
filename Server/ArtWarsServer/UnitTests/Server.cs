using ArtWarsServer.Model;

namespace UnitTests
{
    [TestClass]
    public sealed class ServerFunctions
    {
        //test that the server makes a room code
        [TestMethod]
        public void create_room_code()
        {
            Server server = new Server();

            string rc = server.MakeRoomCode();

            Assert.IsTrue(rc.Length == 4);
        }

        //check if room code can be properly authenticated
        [TestMethod]
        public void verify_room_code_true()
        {
            Server server = new Server();

            server.code = "1234";
            
            Assert.IsTrue(server.verifyRoomCode("1234"));
        }

        //check if room code can be properly authenticated
        [TestMethod]
        public void verify_room_code_false()
        {
            Server server = new Server();

            server.code = "1234";

            Assert.IsFalse(server.verifyRoomCode("5678"));
        }


    }
}
