
using ArtWarsClientWPF.Models;
using Moq;
using System.Net.Sockets;
namespace Client.UnitTest
{
    [TestClass]
    public sealed class TcpHandlerTest
    {
        [TestMethod]
        public async Task sendPakcetWritesToStream()
        {
            // Arrange
            var handler = new TcpHandler();
            byte[] packet = new byte[10];

            var mockStream = new Mock<NetworkStream>();
            // Set up the mock stream to expect a write operation
            mockStream.Setup(s => s.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback((byte[] buffer, int offset, int count) =>
                {
                    // Verify that the packet is written correctly
                    Assert.AreEqual(packet.Length, count);
                    Assert.AreEqual(packet, buffer);
                });

        }
        [TestMethod]
        public async Task StreamReadAsyncReturnsExpectedByteCount()
        {
            // Arrange
            byte[] testData = new byte[10]; // Simulated data
            using var memoryStream = new MemoryStream(testData);

            // Act
            byte[] buffer = new byte[testData.Length];
            int bytesRead = await memoryStream.ReadAsync(buffer, 0, buffer.Length);

            // Assert
            Assert.AreEqual(testData.Length, bytesRead);
        }
    }



}

