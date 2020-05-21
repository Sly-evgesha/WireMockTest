using Newtonsoft.Json;
namespace WireMockTests.Models
{
   public class MockModel
   {
        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("Headers", NullValueHandling = NullValueHandling.Ignore)]
        public string Headers { get; set; }

        [JsonProperty("Status")]
        public long Status { get; set; }

        [JsonProperty("QueryParam", NullValueHandling = NullValueHandling.Ignore)]
        public string QueryParam { get; set; }

        [JsonProperty("QueryMatcher", NullValueHandling = NullValueHandling.Ignore)]
        public string QueryMatcher { get; set; }

        [JsonProperty("QueryPattern", NullValueHandling = NullValueHandling.Ignore)]
        public string QueryPattern { get; set; }

        [JsonProperty("RequestBody", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestBody { get; set; }

        [JsonProperty("RequestHeaderName", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestHeaderName { get; set; }

        [JsonProperty("RequestHeaderValue", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestHeaderValue { get; set; }

        [JsonProperty("Delay", NullValueHandling = NullValueHandling.Ignore)]
        public int Delay { get; set; }

        [JsonProperty("Matcher", NullValueHandling = NullValueHandling.Ignore)]
        public string Matcher { get; set; }
    }
}
