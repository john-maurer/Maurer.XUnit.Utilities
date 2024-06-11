using System;
using System.Collections.Generic;
using System.Linq;
using UnitTesting.MClient.Verbs.Interfaces;

namespace UnitTesting.MClient.Verbs
{
    public class Get : AbstractVerb
    {
        public Get(ref HttpClient client) : base(ref client)
        {

        }

        override public async Task<HttpResponseMessage> Invoke(HttpRequestMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                message.Method = HttpMethod.Get;

                return await base.Invoke(message, cancellationToken);
            }
            catch
            {
                throw;
            }
        }
    }
}
