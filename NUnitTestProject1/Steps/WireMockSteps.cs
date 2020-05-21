using NUnit.Framework;
using System.Threading.Tasks;
using Utilities;
using WireMock.Builders;
using WireMockTests.Models;
using WireMock.WireMock;

namespace WireMockTests.Steps
{
    public static class WireMockSteps
    {
        public static async Task PostFirstMockMapping(MockData mockData)
        {
            Logger.Info("Sending /plaintext/mapping1 mapping to the server mockup via POST request");

            var mockRequestData = MockBuilder.Setup()
                .RequestWith(
                    RequestBuilder.Setup()
                    .WithPath(mockData.Path)
                    .UsingGet()
                    .Build()
                )
                .RespondWith(
                    ResponseBuilder.Setup()
                    .WithStatusCode(mockData.StatusCode)
                    .WithBody(mockData.Body)
                    .WithHeader(mockData.Headers)
                    .Build()
                )
                .Build();

            await WireMockApi.PostMockMapping(mockRequestData);
        }

        public static void AssertResponsesAreEqual(TestResponse actual, TestResponse expected)
        {
            Logger.Info("Checking if responses are correct");

            Assert.AreEqual(actual, expected, "Data are not equal");
        }

        public static async Task PostSecondMockMapping(MockData mockData)
        {
            Logger.Info("Sending /jsontext/mapping2 mapping with params to the server mockup via POST request");

            var mockRequestData2 = MockBuilder.Setup()
                .RequestWith(
                    RequestBuilder.Setup()
                    .WithPath(mockData.Path)
                    .WithParam(mockData.Param, mockData.Pattern)
                    .UsingGet()
                    .Build()
                )
                .RespondWith(
                    ResponseBuilder.Setup()
                    .WithStatusCode(mockData.StatusCode)
                    .WithBody(mockData.Body)
                    .WithHeader(mockData.Headers)
                    .Build()
                )
                .Build();

            await WireMockApi.PostMockMapping(mockRequestData2);
        }

        public static async Task PostThirdMockMapping(MockData mockData)
        {
            Logger.Info("Sending /jsontext/mapping3 mapping with body and headers to the server mockup via POST request");

            var mockRequestData3 = MockBuilder.Setup()
                .RequestWith(
                    RequestBuilder.Setup()
                    .WithPath(mockData.Path)
                    .WithBody(mockData.RequestBody)
                    .WithHeaders(mockData.RequestHeaderName, mockData.RequestHeaderValue)
                    .UsingPost()
                    .Build()
                )
                .RespondWith(
                    ResponseBuilder.Setup()
                    .WithStatusCode(mockData.StatusCode)
                    .WithBody(mockData.Body)
                    .WithHeader(mockData.Headers)
                    .Build()
                )
                .Build();

            await WireMockApi.PostMockMapping(mockRequestData3);
        }

        public static async Task PostFourthMockMapping(MockData mockData)
        {
            Logger.Info("Sending /* mapping with redirection to the server mockup via POST request");

            var mockRequestData4 = MockBuilder.Setup()
                .RequestWith(
                    RequestBuilder.Setup()
                    .WithPath(mockData.Path)
                    .UsingPut()
                    .Build()
                )
                .RespondWith(
                    ResponseBuilder.Setup()
                    .WithStatusCode(mockData.StatusCode)
                    .WithHeader(mockData.RedirectPath)
                    .WithDelay(mockData.Delay)
                    .Build()
                )
                .Build();

            await WireMockApi.PostMockMapping(mockRequestData4);
        }

        public static async Task PrintRequestResponseStatistics()
        {
            Logger.Info("Printing statistics about responses and requests");

            Logger.Info(await WireMockApi.GetRequests());
        }

        public static async Task DeleteAllMappings()
        {
            Logger.Info("Deleteing all mappings at the WireMock server");

            await WireMockApi.DeleteAllMapping();
        }
    }
}
