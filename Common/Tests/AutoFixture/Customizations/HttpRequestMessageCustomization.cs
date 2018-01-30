using AutoFixture;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Common.Tests.AutoFixture.Customizations
{
    public class HttpRequestMessageCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpRequestMessage>(c => c
                .Without(x => x.Content)
                .Do(x => x.Properties[HttpPropertyKeys.HttpConfigurationKey] =
                    new HttpConfiguration()));
        }
    }
}
