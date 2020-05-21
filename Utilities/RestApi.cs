using RestSharp;
using System.Net.Mime;

namespace Utilities
{
    public sealed class RestApi
    {
        static RestApi()
        {
        }

        private RestApi()
        {
        }

        private static RestClient GetInstance(string url)
        {
            return new RestClient(url);
        }

        public static IRestResponse ExecuteRequest(string url, string path, Method method, string data = null,
                                                   string headerName = null, string headerValue = null)
        {
            var request = new RestRequest(path, method);

            if (data != null && method == Method.POST)
            {
                request.AddParameter(MediaTypeNames.Application.Json, data, ParameterType.RequestBody);
            }

            if (headerName != null && headerValue != null)
            {
                request.AddHeader(headerName, headerValue);
            }

            return GetInstance(url).Execute(request);
        }
    }
}
