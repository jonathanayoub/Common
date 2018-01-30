using AutoFixture.NUnit3;
using Common.Tests.AutoFixture.Fixtures;

namespace Common.Tests.AutoFixture.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new AutoMoqDataFixture())
        { }
    }
}
