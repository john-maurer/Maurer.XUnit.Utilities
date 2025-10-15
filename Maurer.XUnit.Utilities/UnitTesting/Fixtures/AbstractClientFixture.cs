using Maurer.XUnit.Utilities;
using Newtonsoft.Json;
using System.Net;

namespace UnitTesting.Fixtures
{
    public class AbstractClientFixture : AbstractFixture
    {
        private MockHttpMessageHandler MockMessageHandler(HttpStatusCode status, string? payload) =>
            new MockHttpMessageHandler((request, token) =>
            {
                HttpResponseMessage result = new HttpResponseMessage(status);

                result.Content = new StringContent(payload!);

                return Task.FromResult(result);
            });

        protected override void Arrange(params object[] parameters)
        {
            OKClient = new HttpClient(MockMessageHandler(HttpStatusCode.OK, JsonConvert.SerializeObject(new object())));
            UnauthorizedClient = new HttpClient(MockMessageHandler(HttpStatusCode.Unauthorized, JsonConvert.SerializeObject(new object())));
            ForbiddenClient = new HttpClient(MockMessageHandler(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(new object())));
            ProxyRequiredClient = new HttpClient(MockMessageHandler((HttpStatusCode)407, JsonConvert.SerializeObject(new object())));
        }

        public HttpClient OKClient { get; protected set; } = null!;
        public HttpClient UnauthorizedClient { get; protected set; } = null!;
        public HttpClient ForbiddenClient { get; protected set; } = null!;
        public HttpClient ProxyRequiredClient { get; protected set; } = null!;
    }
}
