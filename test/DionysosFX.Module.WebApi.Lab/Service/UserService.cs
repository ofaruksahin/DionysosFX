using System;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Module.WebApi.Lab
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
            user.Id = users.Max(f => f.Id);
            users.Add(user);
            return user.Id;
        }

        public bool Update(int id, User user)
        {
            var _user = users.FirstOrDefault(f => f.Id == id);
            if (_user == null)
                return false;
            _user = user;
            return true;
        }

        public bool Delete(int id)
        {
            var _user = users.FirstOrDefault(f => f.Id == id);
            if (_user == null)
                return false;
            users.Remove(_user);
            return true;
        }

        public User Get(int id)
        {
            var _user = users.FirstOrDefault(f => f.Id == id);
            return _user;
        }

        public List<User> GetAll() => users;
    }
}
