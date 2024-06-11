using UnitTesting.MClient;

namespace UnitTesting.Fixtures
{
    public class SoapClientFixture : AbstractClientFixture
    {
        protected override void Arrange(params object[] parameters)
        {
            base.Arrange(parameters);

            OkContext = new SoapClient(OKClient);
            UnauthorizedContext = new SoapClient(UnauthorizedClient);
            ForbiddenContext = new SoapClient(ForbiddenClient);
            ProxyRequiredContext = new SoapClient(ProxyRequiredClient);
        }

        public SoapClientFixture() => Arrange();

        public SoapClient OkContext { get; private set; }
        public SoapClient UnauthorizedContext { get; private set; }
        public SoapClient ForbiddenContext { get; private set; }
        public SoapClient ProxyRequiredContext { get; private set; }
    }
}