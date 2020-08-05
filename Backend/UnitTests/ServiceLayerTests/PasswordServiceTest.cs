using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ServiceLayerTests
{
    [TestClass]
    public class PasswordServiceTest
    {
        PasswordService ps;
        public PasswordServiceTest()
        {
            ps = new PasswordService();
        }

        [TestMethod]
        public void GenerateSalt()
        {
            //Arrange
            //Act
            var salt1 = ps.GenerateSalt();
            var salt2 = ps.GenerateSalt();
            //Assert
            Assert.IsNotNull(salt1);
            Assert.AreNotEqual(salt1, salt2);
        }

        [TestMethod]
        public void HashPassword()
        {
            // Arrange
            var password1Plaintext = "password";
            var salt1 = ps.GenerateSalt();

            var password2Plaintext = "password";
            var salt2 = ps.GenerateSalt();
            // Act
            var hashedPassword1 = ps.HashPassword(password1Plaintext, salt1);
            var hashedPassword2 = ps.HashPassword(password1Plaintext, salt1);

            var hashedPassword3 = ps.HashPassword(password2Plaintext, salt2);

            // Assert
            Assert.IsNotNull(hashedPassword1);
            Assert.IsNotNull(hashedPassword2);
            Assert.IsNotNull(hashedPassword3);
            Assert.AreEqual(hashedPassword1, hashedPassword2);
            Assert.AreNotEqual(hashedPassword1, hashedPassword3);
        }

        [TestMethod]
        public void ValidatePassword_Pass()
        {
            // Arrange
            var password1Plaintext = "password";
            var salt1 = ps.GenerateSalt();
            var hashedPassword1 = ps.HashPassword(password1Plaintext, salt1);

            // Act
            var actual = ps.ValidatePassword(password1Plaintext, salt1, hashedPassword1);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ValidatePassword_Fail()
        {
            // Arrange
            var password1Plaintext = "password";
            var salt1 = ps.GenerateSalt();
            var hashedPassword1 = ps.HashPassword(password1Plaintext, salt1);

            // Act
            var actual = ps.ValidatePassword("password1", salt1, hashedPassword1);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual);
        }
    }
}
