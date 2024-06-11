using UnitTesting.MClient.Verbs.Interfaces;

namespace UnitTesting.MClient.Verbs
{
    public class Delete : AbstractVerb
    {
        public Delete(ref HttpClient client) : base(ref client)
        {

        }

        override public async Task<HttpResponseMessage> Invoke(HttpRequestMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                message.Method = HttpMethod.Delete;

                return await base.Invoke(message, cancellationToken);
            }
            catch
            {
                throw;
            }
        }
    }
}
