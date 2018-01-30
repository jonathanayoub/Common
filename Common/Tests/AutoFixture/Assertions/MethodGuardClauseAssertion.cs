using AutoFixture;
using AutoFixture.Idioms;
using System.Reflection;

namespace Common.Tests.AutoFixture.Assertions
{
    public class MethodGuardClauseAssertion<T> : GuardClauseAssertion
    {
        public MethodGuardClauseAssertion(Fixture fixture) : base(fixture) { }

        public void Verify(string methodName)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(methodName);
            base.Verify(methodInfo);
        }
    }
}
