using DionysosFX.Module.WebApi.Test.Lab.Entities;
using DionysosFX.Module.WebApi.Test.Lab.IService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Module.WebApi.Test.Lab.Service
{
    public class UserService : IUserService
    {
        List<User> users = new List<User>();        

        public void Initialize()
        {
            if (!users.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    User user = new User()
                    {
                        Id = i + 1,
                        Name = Faker.Name.First(),
                        Surname = Faker.Name.Last(),
                        PhoneNumber = Faker.Phone.Number(),
                        Email = Faker.Internet.Email(),
                        BirthDate = DateTime.Now
                    };
                    users.Add(user);
                }
            }
        }

        public int Insert(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
