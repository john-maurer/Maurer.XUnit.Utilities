using UnitTesting.MClient.Verbs.Interfaces;

namespace UnitTesting.MClient.Verbs
{
    public class Post : AbstractVerb
    {
        public Post(ref HttpClient client) : base(ref client)
        {

        }

        override public async Task<HttpResponseMessage> Invoke(HttpRequestMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                message.Method = HttpMethod.Post;

                return await base.Invoke(message, cancellationToken);
            }
            catch
            {
                throw;
            }
        }
    }
}
