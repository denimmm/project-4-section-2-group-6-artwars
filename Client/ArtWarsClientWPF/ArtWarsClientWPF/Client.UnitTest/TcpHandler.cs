
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
            var handler = new TcpHandler();
            byte[] buffer = new byte[10];
            int expectedBytes = buffer.Length;

            var mockStream = new Mock<NetworkStream>();
            // Set up the mock stream to return the expected byte count
            mockStream.Setup(s => s.ReadAsync(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedBytes);
            // Set the mock stream to the handler
            handler._stream = mockStream.Object;
            // Act
            int bytesRead = await handler._stream.ReadAsync(buffer, 0, buffer.Length);
            // Assert
            Assert.AreEqual(expectedBytes, bytesRead);
        }


    }
}
