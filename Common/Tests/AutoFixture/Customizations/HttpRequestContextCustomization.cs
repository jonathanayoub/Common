using AutoFixture;
using System.Web.Http.Controllers;

namespace Common.Tests.AutoFixture.Customizations
{
    public class HttpRequestContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpRequestContext>(c => c.Without(x => x.ClientCertificate));
        }
    }
}
