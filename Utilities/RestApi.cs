using RestSharp;
using System.Net.Mime;

namespace Utilities
{
    public class RestApi
    {
        private readonly RestClient restClient;

        public RestApi(string url)
        {
            restClient = new RestClient(url);
        }

        public IRestResponse ExecuteRequest(string path, Method method, string data = null,
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

            return restClient.Execute(request);
        }
    }
}
