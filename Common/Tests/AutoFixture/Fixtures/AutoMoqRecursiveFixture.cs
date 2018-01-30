using AutoFixture;
using AutoFixture.AutoMoq;
using Common.Tests.AutoFixture.Customizations;

namespace Common.Tests.AutoFixture.Fixtures
{
    public class AutoMoqRecursiveFixture : Fixture
    {
        public AutoMoqRecursiveFixture()
        {
            this.Customize(new AutoMoqCustomization());
            this.Customize(new RecursionBehaviorCustomization());
        }
    }
}
