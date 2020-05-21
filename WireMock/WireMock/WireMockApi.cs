using Newtonsoft.Json;
using RestEase;
using System;
using System.Threading.Tasks;
using Utilities;
using WireMock.Admin.Mappings;
using WireMock.Client;

namespace WireMock.WireMock
{
    public sealed class WireMockApi
    {
        private static readonly string protocol = ConfigUtils.GetValueFromConfig("protocol");
        private static readonly string url = ConfigUtils.GetValueFromConfig("url");
        private static readonly string port = ConfigUtils.GetValueFromConfig("port");

        private static readonly Lazy<IWireMockAdminApi> lazy = 
            new Lazy<IWireMockAdminApi>(() => RestClient.For<IWireMockAdminApi>(Address));

        private static IWireMockAdminApi Instance { get { return lazy.Value; } }

        private WireMockApi() 
        { 
        }

        public static async Task PostMockMapping(MappingModel mappingModel)
        {
            await Instance.PostMappingAsync(mappingModel);
        }

        public static async Task DeleteAllMapping()
        {
            await Instance.DeleteMappingsAsync();
        }

        public static async Task<string> GetRequests()
        {
            var response = await Instance.GetRequestsAsync();
            return JsonConvert.SerializeObject(response);
        }

        public static string Address { get; } = $"{protocol}://{url}:{port}";

    }
}
