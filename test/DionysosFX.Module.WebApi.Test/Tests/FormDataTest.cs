using DionysosFX.Module.Test;
using DionysosFX.Module.WebApi.Lab;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace DionysosFX.Module.WebApi.Test.Tests
{
    public class FormDataTest :BaseTest
    {
        [Fact]
        public void Test_UpdateMethod()
        {
            var response = PatchWithFormData(string.Format("{0}/{1}", Url, "user/updatewithform?id=1"),new { });
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void Test_UpdateMethodWithFormData()
        {
            var user = new User()
            {
                Name = Faker.Name.First(),
                Surname = Faker.Name.Last(),
                Email = Faker.Internet.Email(),
                PhoneNumber = Faker.Phone.Number()
            };
            var response = PatchWithFormData(string.Format("{0}/{1}", Url, "user/updatewithform?id=1"), user);
            if (response.StatusCode != HttpStatusCode.OK)
                Assert.True(false);
            var json = JsonConvert.DeserializeObject<BaseResult<bool>>(response.Content);
            if (json != null)
                Assert.True(true);
        }
    }
}
