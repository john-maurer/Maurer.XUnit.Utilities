using UnitTesting.MClient;

namespace UnitTesting.Fixtures
{
    sealed public class JsonClientFixture : AbstractClientFixture
    {
        protected override void Arrange(params object[] parameters)
        {
            base.Arrange(parameters);

            OkContext = new JsonClient(OKClient);
            UnauthorizedContext = new JsonClient(UnauthorizedClient);
            ForbiddenContext = new JsonClient(ForbiddenClient);
            ProxyRequiredContext = new JsonClient(ProxyRequiredClient);
        }

        public JsonClientFixture() => Arrange();

        public JsonClient? OkContext { get; private set; }
        public JsonClient? UnauthorizedContext { get; private set; }
        public JsonClient? ForbiddenContext { get; private set; }
        public JsonClient? ProxyRequiredContext { get; private set; }
    }
}