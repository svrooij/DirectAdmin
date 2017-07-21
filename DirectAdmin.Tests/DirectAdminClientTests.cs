using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DirectAdmin.Tests
{
    using DirectAdmin.Client;
    using System.Net.Http;
    using System.Threading.Tasks;
    [TestClass]
    public class DirectAdminClientTests
    {
        [TestMethod]
        public async Task ListUsersAsAdmin()
        {
            using (var client = new DirectAdminClient(DemoAdmin))
            {
                var users = await client.ListUsers();

                Assert.IsTrue(users.Count > 0);

            }
        }
        [TestMethod]
        public async Task ListUsersAsReseller()
        {
            using (var client = new DirectAdminClient(DemoReseller))
            {
                try
                {
                    var users = await client.ListUsers();

                }
                catch (DirectAdminClientException e)
                {
                    // This test should give an exception!
                    

                } catch (HttpRequestException e)
                {
                    // This test should give an exception!
                    Console.WriteLine(e.Message);
                }



            }
        }
        [TestMethod]
        public async Task CreateUserAsAdmin()
        {
            using (var client = new DirectAdminClient(DemoAdmin))
            {
                try
                {
                    await client.CreateUser(new Client.RequestOptions.CreateUserOptions
                    {
                        DefaultIpAddress = "123.123.23.45",
                        Domain = "testuser.com",
                        Email = "test@testuser.com",
                        NotifyUser = false,
                        PackageName = "package-1",
                        Password = "testpwd123",
                        Username = "testuser",
                        UsernameOwner = "demo_reseller"
                    });
                }
                catch (DirectAdminClientException e)
                {
                    if(!e.NotInDemo)
                        Assert.Fail(e.Message);
                }
                
                
                
            }
        }

        [TestMethod]
        public async Task DeleteUser()
        {
            using (var client = new DirectAdminClient(DemoAdmin))
            {
                try
                {
                    await client.DeleteUser("demo_user");
                }
                catch (DirectAdminClientException e)
                {
                    if (!e.NotInDemo)
                        Assert.Fail(e.Message);
                }



            }
        }

        private static DirectAdminClientOptions DemoAdmin
        {
            get
            {
                return new DirectAdminClientOptions
                {
                    ServerName = "www.directadmin.com",
                    UseHttps = true,
                    Port = 2222,
                    Username = "demo_admin",
                    Password="demo"
                };
            }
        }

        private static DirectAdminClientOptions DemoReseller
        {
            get
            {
                return new DirectAdminClientOptions
                {
                    ServerName = "www.directadmin.com",
                    UseHttps = true,
                    Port = 2222,
                    Username = "demo_reseller",
                    Password = "demo"
                };
            }
        }
    }
}
