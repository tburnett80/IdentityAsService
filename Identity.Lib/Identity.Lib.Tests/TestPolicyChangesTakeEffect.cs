using System;
using System.Threading.Tasks;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Contracts.Managers;
using Identity.Lib.Public.Extensions;
using Identity.Lib.Public.Models.Identity;
using Identity.Lib.Public.Models.Policy;
using Identity.Lib.Tests.TestImplimentations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Identity.Lib.Tests
{
    [TestCategory("Integration Tests")]
    [TestClass]
    public class TestPolicyChangesTakeEffect
    {
        private const string ConnStr = "Server=.;Database=IdenityTest{0};Integrated Security=True;MultipleActiveResultSets=True";
        private static IServiceProvider Provider { get; set; }
        private static TestSharedPolicyAccessor Shared { get; set; }

        [ClassInitialize]
        public static void Init(TestContext tctx)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddIdentityStoreConfigSqlServer(string.Format(ConnStr, $"{Guid.NewGuid()}"));
            services.AddCustomIdentityProviders();

            Shared = new TestSharedPolicyAccessor();
            services.AddSingleton<ISharedPolicyAccessor>(Shared);
            services.AddTransient<IOptions<IdentityOptions>>(ctx => Options.Create(Shared._opts));

            Provider = services.BuildServiceProvider();
            Provider.EnsureUserStoreCreated();
        }

        [ClassCleanup]
        public static void Teardown()
        {
            Provider.EnsureUserStoreDeleted();
        }

        [TestMethod]
        public async Task ValidatePolicyChangesGoIntoEffect()
        {
            //create user with default policy
            using (var mgr = Provider.GetRequiredService<IUserWriteManager>())
            {
                mgr.CreateNewUser(new User
                {
                    UserName = "TestUser",
                    Email = "test@test.com",
                    Password = "Test1!"
                }).Wait();
            }

            //Change the policy
            using (var polMgr = Provider.GetRequiredService<IPolicyWriteManager>())
            {
                await polMgr.SetPasswordPolicy(new PasswordPolicy
                {
                    RequireDigit = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredUniqueChars = 2,
                    RequiredLength = 4,
                    CreatedById = 1
                });
            }

            //create user with password violating policy
            using (var mgr = Provider.GetRequiredService<IUserWriteManager>())
            {
                var usr2 = await mgr.CreateNewUser(new User
                {
                    UserName = "TestUser2",
                    Email = "test2@test.com",
                    Password = "ttt"
                });

                Assert.IsTrue(usr2.IsFailure, "password is bad, needs another unique char and length greater than 3");
                Assert.IsInstanceOfType(usr2.Exception, typeof(AggregateException), "Should be matching type");
                Assert.AreEqual(2, ((AggregateException)usr2.Exception).InnerExceptions.Count, "Should be two exceptions");
            }

            //create user with valid password
            using (var mgr = Provider.GetRequiredService<IUserWriteManager>())
            {
                var usr3 = await mgr.CreateNewUser(new User
                {
                    UserName = "TestUser3",
                    Email = "test3@test.com",
                    Password = "ttty"
                });

                Assert.IsFalse(usr3.IsFailure, "Password should pass policy now");
            }
        }
    }
}
