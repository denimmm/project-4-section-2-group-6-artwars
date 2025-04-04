
using ArtWarsClientWPF.Models;
namespace Client.UnitTest
{
    [TestClass]
    public sealed class PlayerTest
    {
        [TestMethod]
        public void Player_DefaultConstructor_InitialValuesAreCorrect()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.AreEqual(string.Empty, player.Name);
            Assert.AreEqual("-1", player.Id);
            Assert.AreEqual(0, player.score);
        }
        [TestMethod]
        public void SetPlayerName_UpdatesName()
        {
            // Arrange
            var player = new Player();
            string expectedName = "TestPlayer";

            // Act
            player.setPlayerName(expectedName);

            // Assert
            Assert.AreEqual(expectedName, player.getPlanyerName());
        }
        [TestMethod]
        public void SetScore_UpdatesScore()
        {
            // Arrange
            var player = new Player();
            int expectedScore = 100;

            // Act
            player.setScore(expectedScore);

            // Assert
            Assert.AreEqual(expectedScore, player.getScore());
        }
        [TestMethod]
        public void SetPlayerId_UpdatesId()
        {
            // Arrange
            var player = new Player();
            string expectedId = "12345";

            // Act
            player.setPlayerId(expectedId);

            // Assert
            Assert.AreEqual(expectedId, player.getPlayerId());
        }
    }
}
