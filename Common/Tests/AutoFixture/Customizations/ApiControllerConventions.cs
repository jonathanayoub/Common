using AutoFixture;
using AutoFixture.AutoMoq;

namespace Common.Tests.AutoFixture.Customizations
{
    public class ApiControllerConventions : CompositeCustomization
    {
        public ApiControllerConventions()
            : base(
                new HttpRequestMessageCustomization(),
                new ApiControllerCustomization(),
                new AutoMoqCustomization(),
                new HttpRequestContextCustomization())
        {
        }
    }
}
