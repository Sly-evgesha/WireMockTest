namespace WireMockTests.Models
{
    public class TestResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Headers { get; set; }

        public TestResponse(string status, string message, string headers)
        {
            Status = status;
            Message = message;
            Headers = headers;
        }

        public override bool Equals(object obj)
        {
            return obj is TestResponse data &&
                   Status == data.Status &&
                   Message == data.Message &&
                   Headers == data.Headers;
        }

        public override int GetHashCode()
        {
            return Status.GetHashCode() ^
                Message.GetHashCode() ^
                Headers.GetHashCode();
        }
    }
}
