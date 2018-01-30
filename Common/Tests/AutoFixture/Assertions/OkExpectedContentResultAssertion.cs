using FluentAssertions;
using NUnit.Framework;
using System.Web.Http;
using System.Web.Http.Results;

namespace Common.Tests.AutoFixture.Assertions
{
    public class OkExpectedContentResultAssertion<TContent>
    {
        private readonly TContent expectedContent;
        private readonly IHttpActionResult resultWithContent;

        public OkExpectedContentResultAssertion(IHttpActionResult resultWithContent, TContent expectedContent)
        {
            Guard.NotNull(() => resultWithContent, resultWithContent);
            Guard.NotNull(() => expectedContent, expectedContent);
            this.resultWithContent = resultWithContent;
            this.expectedContent = expectedContent;
        }

        public void Verify()
        {
            var result = this.resultWithContent as OkNegotiatedContentResult<TContent>;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            result.Content.ShouldBeEquivalentTo(expectedContent);
        }
    }
}
