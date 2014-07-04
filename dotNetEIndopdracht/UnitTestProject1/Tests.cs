using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperStoreLibrary.Communication;
using SuperStoreLibrary.Util;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void PasswordTest()
        {
            string Username = "Robin";
            string Password = PasswordUtils.GeneratePassword(Username);
            Assert.IsTrue(Password == "Spcjo");
        }
    }
}
