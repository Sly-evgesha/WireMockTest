using WireMock.Admin.Mappings;

namespace WireMock.Builders
{
    public class MockBuilder
    {
        private readonly MappingModel mappingModel = new MappingModel();

        public static MockBuilder Setup()
        {
            return new MockBuilder();
        }

        public MockBuilder RequestWith(RequestModel request)
        {
            mappingModel.Request = request;
            return this;
        }

        public MockBuilder RespondWith(ResponseModel response)
        {
            mappingModel.Response = response;
            return this;
        }

        public MappingModel Build()
        {
            return mappingModel;
        }
    }
}
