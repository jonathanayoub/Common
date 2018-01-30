using AutoFixture.NUnit3;
using Common.Tests.AutoFixture.Fixtures;

namespace Common.Tests.AutoFixture.Attributes
{
    public class AutoMoqRecursiveData : AutoDataAttribute
    {
        public AutoMoqRecursiveData()
            : base(new AutoMoqRecursiveFixture())
        {
        }
    }
}
