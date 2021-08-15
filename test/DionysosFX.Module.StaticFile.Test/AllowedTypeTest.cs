using DionysosFX.Module.Test;
using System.Net;
using Xunit;

namespace DionysosFX.Module.StaticFile.Test
{
    public class AllowedTypeTest : BaseTest
    {
        [Fact]
        public void Test_HtmlMimeType()
        {
            var response = Get(string.Format("{0}/{1}",Url,"index.html"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void Test_JsMimeType()
        {
            var response = Get(string.Format("{0}/{1}", Url, "index.js"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void Test_NotAllowedMimeType()
        {
            var response = Get(string.Format("{0}/{1}", Url, "index.json"));
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("aksldfmlsakfdsaf.json")]
        [InlineData("ajsfdsajfldsa.csv")]
        [InlineData("klmsdgskdgsfd.css")]
        public void Test_NotFoundFile(string file)
        {
            var response = Get(string.Format("{0}/{1}", Url, file));
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

    }
}
