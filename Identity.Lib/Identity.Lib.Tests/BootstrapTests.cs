using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identity.Lib.Public.Extensions;

namespace Identity.Lib.Tests
{
    [TestClass]
    public class BootstrapTests
    {
        private const string ConnStr = "Server=.;Database=IdenityTest{0};Integrated Security=True;MultipleActiveResultSets=True";
        private static IServiceProvider Provider { get; set; }

        [ClassInitialize]
        public static void Init(TestContext tctx)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddIdentityStoreConfigSqlServer(string.Format(ConnStr, $"{Guid.NewGuid()}"));
            Provider = services.BuildServiceProvider();
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void TestSetupTeardown()
        {
            Assert.IsTrue(Provider.EnsureUserStoreCreated(), "Should return true when db created");
            Assert.IsTrue(Provider.EnsureUserStoreDeleted(), "Should return true when database deleted");
        }
    }
}
