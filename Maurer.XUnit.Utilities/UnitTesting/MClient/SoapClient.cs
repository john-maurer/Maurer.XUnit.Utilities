using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnitTesting.MClient.Verbs.Interfaces;

namespace UnitTesting.MClient
{
    public class SoapClient : ISendMessage
    {
        protected HttpClient _httpClient;

        public SoapClient(HttpClient client) => _httpClient = client;

        virtual public async Task<HttpResponseMessage> SendRequestAsync<TVerb>(string soapAction, object content, CancellationToken cancellationToken = new CancellationToken()) where TVerb : AbstractVerb
        {
            HttpResponseMessage response;

            var xmlSerializer = new XmlSerializer(content.GetType());

            using (var message = new HttpRequestMessage())
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(stringWriter))
                    {
                        xmlSerializer.Serialize(writer, content);

                        message.Content = new StringContent(stringWriter.ToString(), Encoding.UTF8, "text/xml");
                        message.Content.Headers.Add("SOAPAction", soapAction);
                        message.RequestUri = new Uri(soapAction);

                        response =
                            await((TVerb)typeof(TVerb).GetConstructor(new Type[] { typeof(HttpClient).MakeByRefType() })!
                                .Invoke(new object[] { _httpClient }))
                                    .Invoke(message, cancellationToken);
                    }
                }
            }

            return response;
        }
    }
}
