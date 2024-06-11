namespace UnitTesting.MClient.Verbs.Interfaces
{
    abstract public class AbstractVerb : IVerb
    {
        protected readonly HttpClient _client;

        private AbstractVerb() => _client = new HttpClient();

        public AbstractVerb(ref HttpClient client) => _client = client;

        virtual public async Task<HttpResponseMessage> Invoke(HttpRequestMessage message, CancellationToken cancellationToken = new CancellationToken()) =>
            await _client.SendAsync(message, cancellationToken);
    }
}
