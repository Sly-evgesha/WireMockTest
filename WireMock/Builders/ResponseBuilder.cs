using System.Collections.Generic;
using WireMock.Admin.Mappings;

namespace WireMock.Builders
{
    public class ResponseBuilder
    {
        private readonly ResponseModel response = new ResponseModel();

        private ResponseBuilder()
        {
        }

        public static ResponseBuilder Setup()
        {
            return new ResponseBuilder();
        }

        public ResponseModel Build()
        {
            return response;
        }

        public ResponseBuilder WithStatusCode(long statusCode)
        {
            response.StatusCode = statusCode;
            return this;
        }

        public ResponseBuilder WithBody(string body)
        {
            response.Body = body;
            return this;
        }

        public ResponseBuilder WithHeader(string contentType)
        {
            response.Headers = new Dictionary<string, object>()
            {
                {"Content-Type",  contentType}
            };
            return this;
        }

        public ResponseBuilder WithDelay(int delay)
        {
            response.Delay = delay;
            return this;
        }
    }
}
