using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;

namespace UnitTests
{
    [TestClass]
    public class CryptoServiceTest
    {

        [TestMethod]
        public void GenerateToken_Pass()
        {
            // Arrange
            int expectedLength = 64;
            // Act
            var token = CryptoService.GenerateToken();
            // Assert
            Assert.AreEqual(token.Length, expectedLength);
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void GenerateToken_Fail()
        {
            // Arrange
            int expectedLength = 63;
            // Act
            var token = CryptoService.GenerateToken();
            // Assert
            Assert.AreNotEqual(token.Length, expectedLength);
        }
    }
}
