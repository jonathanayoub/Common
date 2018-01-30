using AutoFixture;
using AutoFixture.NUnit3;
using Common.Tests.AutoFixture.Customizations;

namespace Common.Tests.AutoFixture.Attributes
{
    public class AutoMoqApiDataAttribute : AutoDataAttribute
    {
        public AutoMoqApiDataAttribute() :
            base(new Fixture()
                .Customize(new ApiControllerConventions()))
        { }
    }
}
