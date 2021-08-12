using DionysosFX.Module.Test;
using DionysosFX.Module.WebApi.Lab;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace DionysosFX.Module.WebApi.Test.Tests
{
    public class QueryDataTest : BaseTest
    {
        [Fact]
        public void Test_ListMethod()
        {
            var response = Get(string.Format("{0}/{1}", Url, "user/list"));
            if (response.StatusCode == HttpStatusCode.NotFound)
                Assert.True(false);
            var json = JsonConvert.DeserializeObject(response.Content,typeof(BaseResult<List<User>>));
            if (json != null)
                Assert.True(true);
        }

        [Fact]
        public void Test_ListMethodWithId()
        {
            var response = Get(string.Format("{0}/{1}", Url, "user/list?id=1"));
            if (response.StatusCode == HttpStatusCode.NotFound)
                Assert.True(false);
            var json = JsonConvert.DeserializeObject(response.Content, typeof(BaseResult<User>));
            if (json != null)
                Assert.True(true);
        }

        [Fact]
        public void Test_ListMethodWithNonNumericId()
        {
            var response = Get(string.Format("{0}/{1}", Url, "user/list?Id=abc"));
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Fact]
        public void Test_ListMethodWithNonId()
        {
            var response = Get(string.Format("{0}/{1}", Url, "user/list?Id="));
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Fact]
        public void Test_DeleteMethod()
        {
            var response = Delete(string.Format("{0}/{1}", Url, "user/delete?id="));
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Fact]
        public void Test_DeleteMethodWithId()
        {
            var response = Delete(string.Format("{0}/{1}", Url, "user/delete?id=1"));
            if (response.StatusCode == HttpStatusCode.NotFound)
                Assert.True(false);
            var json = JsonConvert.DeserializeObject(response.Content, typeof(BaseResult<bool>));
            if (json != null)
                Assert.True(true);
        }
    }
}
