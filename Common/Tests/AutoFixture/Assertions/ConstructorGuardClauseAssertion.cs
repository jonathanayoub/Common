using AutoFixture;
using AutoFixture.Idioms;
using System.Collections.Generic;
using System.Reflection;

namespace Common.Tests.AutoFixture.Assertions
{
    public class ConstructorGuardClauseAssertion<T> : GuardClauseAssertion
    {
        public ConstructorGuardClauseAssertion(Fixture fixture) : base(fixture) { }

        public void Verify()
        {
            IEnumerable<ConstructorInfo> constructorInfos = typeof(T).GetConstructors();
            base.Verify(constructorInfos);
        }
    }
}
