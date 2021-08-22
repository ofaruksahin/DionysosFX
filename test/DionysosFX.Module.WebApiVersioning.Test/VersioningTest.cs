using DionysosFX.Module.Test;
using System.Net;
using Xunit;

namespace DionysosFX.Module.WebApiVersioning.Test
{
    public class VersioningTest : BaseTest
    {
        [Theory]
        [InlineData("0.0.0.1")]
        [InlineData("0.0.0.2")]
        [InlineData("0.0.0.3")]
        public void TestDeprecatedVersion(string version)
        {
            Headers.Add("X-Api-Version", version);
            var response = Get(string.Format("{0}/{1}", Url, "user/list"));
            Assert.True(response.Content.Contains($"{version} is deprecated"));
        }

        [Theory]
        [InlineData("0.0.0.4")]
        [InlineData("0.0.0.5")]
        public void TestAllowedVersion(string version)
        {
            Headers.Add("X-Api-Version", version);
            var response = Get(string.Format("{0}/{1}", Url, "user/list"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("0.0.0.6")]
        public void TestNotAllowedVersion(string version)
        {
            Headers.Add("X-Api-Version", version);
            var response = Get(string.Format("{0}/{1}", Url, "user/list"));
            Assert.True(response.Content.Contains($"{version} is not defined"));
        }

        [Fact]
        public void TestWithoutVersion()
        {
            var response = Get(string.Format("{0}/{1}", Url, "user/list"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
