using DionysosFX.ProjectTemplate.WebAPI.Model;
using System.Collections.Generic;

namespace DionysosFX.ProjectTemplate.WebAPI
{
    public class UserData
    {
        private static volatile UserData _userData;
        private static object _syncLock = new object();

        public static UserData Instance
        {
            get
            {
                lock (_syncLock)
                {
                    return _userData ?? (_userData = new UserData());
                }
            }
        }

        public List<UserModel> Users;
        public UserData()
        {
            Users = new List<UserModel>();
            for (int i = 0; i < 100; i++)
            {
                Users.Add(new UserModel()
                {
                    Email = Faker.Internet.Email(),
                    Password = Faker.Internet.UserName()
                });
            }
        }
    }
}
