using Newtonsoft.Json;
using UnitTesting.MClient.Verbs.Interfaces;

namespace UnitTesting.MClient
{
    public class JsonClient : ISendMessage
    {
        protected HttpClient _httpClient;

        public JsonClient(HttpClient client) => _httpClient = client;

        virtual public async Task<HttpResponseMessage> SendRequestAsync<TVerb>(string uri, object content, CancellationToken cancellationToken = new CancellationToken()) where TVerb : AbstractVerb
        {
            HttpResponseMessage response;

            using (var message = new HttpRequestMessage())
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(content));
                message.RequestUri = new Uri(uri);

                response = 
                    await ((TVerb) typeof(TVerb).GetConstructor(new Type[] { typeof(HttpClient).MakeByRefType() })!
                        .Invoke(new object[] { _httpClient }))
                            .Invoke(message, cancellationToken);
            }

            return response;
        }
    }
}
