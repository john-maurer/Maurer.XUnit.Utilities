using UnitTesting.MClient.Verbs.Interfaces;

namespace UnitTesting.MClient
{
    public interface ISendMessage
    {
        Task<HttpResponseMessage> SendRequestAsync<TVerb>
            (string uri, object content, CancellationToken cancellationToken = new CancellationToken()) 
                where TVerb : AbstractVerb;
    }
}