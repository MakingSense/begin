using System.Diagnostics;
using BeginMobile.Services.ManagerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeginMobile.Services.Tests
{
    [TestClass]
    public class LoginUserManagerTests
    {
        private const int InfiniteTimeout = 60000;

        [TestMethod]
        public void SignUpTest()
        {
            var username = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var manager = new LoginUserManager();

            var time1 = DateTime.UtcNow;
            var task = manager.Register(username, username + "@mail.com", username, username);

            var hasNotTimedOut = task.Wait(InfiniteTimeout);
            var time2 = DateTime.UtcNow;
            var user = task.Result;
            
            Debug.WriteLine("Delay Signup: '{0}' ms. Timed out: '{1}'", time2.Subtract(time1).TotalMilliseconds, !hasNotTimedOut);
            Assert.IsTrue(hasNotTimedOut, "It has timed out");
            Assert.IsNotNull(user, "User has not been created");
            Assert.IsFalse(user.HasError, user.Error);
        }
    }
}
