using DionysosFX.Module.Test;
using DionysosFX.Module.WebApi.Lab;
using Newtonsoft.Json;
using System;
using System.Net;
using Xunit;

namespace DionysosFX.Module.WebApi.Test.Tests
{
    public class JsonDataTest : BaseTest
    {
        [Fact]
        public void Test_InsertMethodEmptyBody()
        {
            var response = Post(string.Format("{0}/{1}", Url, "insert"), null);
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Fact]
        public void Test_InsertMethodWithData()
        {
            var user = new User()
            {
                Name = Faker.Name.First(),
                Surname = Faker.Name.Last(),
                Email = Faker.Internet.Email(),
                PhoneNumber = Faker.Phone.Number(),
                BirthDate = DateTime.Now
            };
            var response = Post(string.Format("{0}/{1}", Url, "user/insert"), user);
            if (response.StatusCode != HttpStatusCode.OK)
                Assert.True(false);
            var json = JsonConvert.DeserializeObject<BaseResult<int>>(response.Content);
            Assert.True(json.Data > 0);
        }

        [Fact]
        public void Test_UpdateMethodEmptyBody()
        {
            var response = Patch(string.Format("{0}/{1}", Url, "update?id=1"), null);
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Fact]
        public void Test_UpdateMethodWithData()
        {
            var user = new User()
            {
                Name = Faker.Name.First(),
                Surname = Faker.Name.Last(),
                Email = Faker.Internet.Email(),
                PhoneNumber = Faker.Phone.Number(),
                BirthDate = DateTime.Now
            };
            var response = Patch(string.Format("{0}/{1}", Url, "user/update?id=1"), user);
            if (response.StatusCode != HttpStatusCode.OK)
                Assert.True(false);
            var json = JsonConvert.DeserializeObject<BaseResult<bool>>(response.Content);
            Assert.True(json.Data);
        }
    }
}
