using System.Collections.Generic;
using WireMock.Admin.Mappings;

namespace WireMock.Builders
{
    public class RequestBuilder
    {
        private readonly RequestModel request = new RequestModel();

        private RequestBuilder()
        {
        }

        public static RequestBuilder Setup()
        {
            return new RequestBuilder();
        }

        public RequestModel Build()
        {
            return request;
        }

        public RequestBuilder WithPath(string path)
        {
            request.Path = path;
            return this;
        }

        public RequestBuilder UsingGet()
        {
            request.Methods = new[] { "get" };
            return this;
        }

        public RequestBuilder UsingPost()
        {
            request.Methods = new[] { "post" };
            return this;
        }

        public RequestBuilder UsingPut()
        {
            request.Methods = new[] { "put" };
            return this;
        }

        public RequestBuilder WithBody(string value, string name = "RegexMatcher")
        {
            request.Body = new BodyModel
            {
                Matcher = new MatcherModel
                {
                    Name = name,
                    Pattern = value
                }
            };

            return this;
        }

        public RequestBuilder WithHeaders(string headerName, string value, string name = "WildcardMatcher")
        {
            request.Headers = new List<HeaderModel>
            {
                new HeaderModel
                {
                    Name = headerName,
                    Matchers = new List<MatcherModel>
                    {
                        new MatcherModel
                        {
                            Name = name,
                            Pattern = value
                        }
                    }
                }
            };

            return this;
        }

        public RequestBuilder WithParam(string paramName, string value, string name = "WildcardMatcher")
        {
            request.Params = new List<ParamModel>
            { new ParamModel
                {
                    Name = paramName,
                    Matchers = new MatcherModel[]
                    {
                        new MatcherModel
                        {
                            Name = name,
                            Pattern = value
                        }
                    }
                }
            };
            return this;
        }
    }
}
