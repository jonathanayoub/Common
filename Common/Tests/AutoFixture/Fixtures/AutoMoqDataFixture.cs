using AutoFixture;
using AutoFixture.AutoMoq;

namespace Common.Tests.AutoFixture.Fixtures
{
    public class AutoMoqDataFixture : Fixture
    {
        public AutoMoqDataFixture()
        {
            this.Customize(new AutoMoqCustomization());
        }
    }
}
