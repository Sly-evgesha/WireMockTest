using NUnit.Framework;
using Utilities;
using WireMock.Server;
using WireMock.Settings;
using WireMock.WireMock;

namespace WireMockTests.Tests
{
    public abstract class BaseTest
    {
        private WireMockServer Server { get; set; }

        public static string Address { get; set; }

        public RestApi RestApi { get; set; }

        [SetUp]
        public void StartMockServer()
        {
            Address = WireMockApi.Address;

            Server = WireMockServer.Start(new FluentMockServerSettings
            {
                Urls = new[] { Address },
                StartAdminInterface = true,
                AllowCSharpCodeMatcher = true
            });

            RestApi = new RestApi(Address);
        }

        [TearDown]
        public void ShutdownServer()
        {
            Server.Stop();
        }
    }
}
