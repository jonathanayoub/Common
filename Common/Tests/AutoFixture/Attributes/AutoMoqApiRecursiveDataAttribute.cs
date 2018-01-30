using AutoFixture;
using AutoFixture.NUnit3;
using Common.Tests.AutoFixture.Customizations;

namespace Common.Tests.AutoFixture.Attributes
{
    public class AutoMoqApiRecursiveDataAttribute : AutoDataAttribute
    {
        public AutoMoqApiRecursiveDataAttribute() :
            base(new Fixture()
                .Customize(new ApiControllerConventions())
                .Customize(new RecursionBehaviorCustomization()))
        { }
    }
}
