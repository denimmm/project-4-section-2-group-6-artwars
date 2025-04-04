
using ArtWarsClientWPF.Models;
namespace Client.UnitTest
{
    [TestClass]
    public sealed class ClientUnitTest
    {
        [TestMethod]
        public void Client_Constructor_InitializesCorrectly()
        {
            // Arrange
            string expectedName = "TestPlayer";
            string expectedRoomCode = "1234";

            // Act

            ArtWarsClientWPF.Models.Client client = new ArtWarsClientWPF.Models.Client(expectedName, expectedRoomCode);

            // Assert
            Assert.AreEqual("-1", client.player.Id);
            Assert.AreEqual(expectedName, client.player.Name);
            Assert.AreEqual("", client.state);
            Assert.AreEqual(expectedRoomCode, client.roomCode);
        }

        [TestMethod]
        public void SetState_UpdatesState()
        {
            // Arrange
            var client = new ArtWarsClientWPF.Models.Client("TestPlayer", "1234");
            string expectedState = "Connected";

            // Act
            client.setState(expectedState);

            // Assert
            Assert.AreEqual(expectedState, client.getState());
        }

        [TestMethod]
        public void GetPlayer_ReturnsPlayerInstance()
        {
            // Arrange
            var client = new ArtWarsClientWPF.Models.Client("TestPlayer", "12345");

            // Act
            var player = client.getPlayer();

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual("TestPlayer", player.Name);
        }
    }
    
}
