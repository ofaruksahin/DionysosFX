using DionysosFX.Module.FluentValidator.Lab.Entities;
using DionysosFX.Module.Test;
using System.Net;
using Xunit;

namespace DionysosFX.Module.FluentValidator.Test
{
    public class FluentValidationTest : BaseTest
    {
        [Fact]
        public void Test_BadRequest()
        {
            User user = new User()
            {
                Name = Faker.Name.First(),
            };
            var response = Post(string.Format("{0}/{1}", Url, "user/insert"), user);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Test_BadRequest2()
        {
            User user = new User()
            {
                Name = Faker.Name.First(),
                Surname = Faker.Name.Last()
            };
            var response = Post(string.Format("{0}/{1}", Url, "user/insert"), user);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Test_ValidRequest()
        {
            User user = new User()
            {
                Name = Faker.Name.First(),
                Surname = Faker.Name.Last(),
                Age = 18
            };
            var response = Post(string.Format("{0}/{1}", Url, "user/insert"), user);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
