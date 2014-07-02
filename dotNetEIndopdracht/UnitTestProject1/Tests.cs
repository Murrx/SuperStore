using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperStoreLibrary.Communication;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void PasswordTest()
        {
            string Username = "Robin";
            string Password = SuperStoreServiceImplementation.GeneratePassword(Username);
            Assert.IsTrue(Password == "Spcjo");
        }
    }
}
