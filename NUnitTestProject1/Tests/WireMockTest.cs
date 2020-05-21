using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utilities;
using static WireMockTests.Constants.MappingConstants;
using WireMockTests.Models;
using WireMockTests.Steps;

namespace WireMockTests.Tests
{
    public class WireMockTests: BaseTest
    {
        private static readonly string PathToTestData = PathUtil.GetPathToFile(@"TestResources\testConfig.json");
        private static readonly List<MockModel> mockModel = JsonConvert.DeserializeObject<List<MockModel>>(File.ReadAllText(PathToTestData));
        private static readonly string Mapping1Url = mockModel[FirstMappingModel].Url;
        private static readonly string Mapping2Url = mockModel[SecondMappingModel].Url;
        private static readonly string Mapping3Url = mockModel[ThirdMappingModel].Url;
        private static readonly string Mapping4Url = mockModel[FourthMappingModel].Url;
        private static readonly string Mapping1Message = RandomElement.GetRandomString(100);
        private static readonly string Mapping2Message = File.ReadAllText(PathUtil.GetPathToFile(@"Resources\config.json"));
        private static readonly string Mapping3Message = RandomElement.GetRandomString(20);
        private static readonly string Mapping1Headers = mockModel[FirstMappingModel].Headers;
        private static readonly string Mapping2Headers = mockModel[SecondMappingModel].Headers;
        private static readonly string Mapping3Headers = mockModel[ThirdMappingModel].Headers;
        private static readonly long Mapping1Status = mockModel[FirstMappingModel].Status;
        private static readonly long Mapping2Status = mockModel[SecondMappingModel].Status;
        private static readonly long Mapping3Status = mockModel[ThirdMappingModel].Status;
        private static readonly long Mapping4Status = mockModel[FourthMappingModel].Status;
        private static readonly string QueryParam = mockModel[SecondMappingModel].QueryParam;
        private static readonly string QueryMatcher = mockModel[SecondMappingModel].QueryMatcher;
        private static readonly string RegexMatcher = mockModel[FourthMappingModel].Matcher;
        private static readonly string QueryPattern = mockModel[SecondMappingModel].QueryPattern;
        private static readonly string Mapping2UrlWithQuery = $"{Mapping2Url}?{QueryParam}={QueryPattern}";
        private static readonly string Mapping4UrlToMapping1 = $"{Address}{Mapping1Url}";
        private static readonly string RequestBody = mockModel[ThirdMappingModel].RequestBody;
        private static readonly string RequestHeaderName = mockModel[ThirdMappingModel].RequestHeaderName;
        private static readonly string RequestHeaderValue = mockModel[ThirdMappingModel].RequestHeaderValue;
        private static readonly int Delay = mockModel[FourthMappingModel].Delay;
        private static readonly string ExpectedStatus1 = "OK";
        private static readonly string ExpectedStatus2 = "OK";
        private static readonly string ExpectedStatus3 = "InternalServerError";
        private static readonly string ExpectedStatus4 = "Redirect";
        private static readonly string ExpectedBody4 = string.Empty;

        [Test]
        public async Task RunTest()
        {
            var testData1 = new MockData
            {
                Address = Address,
                Path = Mapping1Url,
                StatusCode = Mapping1Status,
                Body = Mapping1Message,
                Headers = Mapping1Headers
            };

            await WireMockSteps.PostFirstMockMapping(testData1);

            var response1 = RestApi.ExecuteRequest(url: Address, path: Mapping1Url, Method.GET);
            var actualData1 = new TestResponse(response1.StatusCode.ToString(), response1.Content, response1.ContentType);
            var expectedData1 = new TestResponse(ExpectedStatus1, Mapping1Message, Mapping1Headers);

            WireMockSteps.AssertResponsesAreEqual(actualData1, expectedData1);

            var testData2 = new MockData
            {
                Address = Address,
                Path = Mapping2Url,
                StatusCode = Mapping2Status,
                Body = Mapping2Message,
                Headers = Mapping2Headers,
                Param = QueryParam,
                Matcher = QueryMatcher,
                Pattern = QueryPattern
            };

            await WireMockSteps.PostSecondMockMapping(testData2);

            var response2 = RestApi.ExecuteRequest(url: Address, path: Mapping2UrlWithQuery, Method.GET);
            var actualData2 = new TestResponse(response2.StatusCode.ToString(), response2.Content, response2.ContentType);
            var expectedData2 = new TestResponse(ExpectedStatus2, Mapping2Message, Mapping2Headers);

            WireMockSteps.AssertResponsesAreEqual(actualData2, expectedData2);

            var testData3 = new MockData
            {
                Address = Address,
                Path = Mapping3Url,
                StatusCode = Mapping3Status,
                Body = Mapping3Message,
                Headers = Mapping3Headers,
                RequestBody = RequestBody,
                RequestHeaderName = RequestHeaderName,
                RequestHeaderValue = RequestHeaderValue
            };

            await WireMockSteps.PostThirdMockMapping(testData3);

            var response3 = RestApi.ExecuteRequest(Address, Mapping3Url, Method.POST, RequestBody, RequestHeaderName, RequestHeaderValue);
            var actualData3 = new TestResponse(response3.StatusCode.ToString(), response3.Content, response3.ContentType);
            var expectedData3 = new TestResponse(ExpectedStatus3, Mapping3Message, Mapping3Headers);

            WireMockSteps.AssertResponsesAreEqual(actualData3, expectedData3);

            var testData4 = new MockData
            {
                Address = Address,
                Path = Mapping4Url,
                RedirectPath = Mapping4UrlToMapping1,
                Delay = Delay,
                Matcher = RegexMatcher,
                StatusCode = Mapping4Status
            };

            await WireMockSteps.PostFourthMockMapping(testData4);

            var response4 = RestApi.ExecuteRequest(Address, Mapping4Url, Method.PUT);
            var actualData4 = new TestResponse(response4.StatusCode.ToString(), response4.Content, response4.ContentType);
            var expectedData4 = new TestResponse(ExpectedStatus4, ExpectedBody4, Mapping4UrlToMapping1);

            WireMockSteps.AssertResponsesAreEqual(actualData4, expectedData4);

            await WireMockSteps.PrintRequestResponseStatistics();

            await WireMockSteps.DeleteAllMappings();
        }
    }
}