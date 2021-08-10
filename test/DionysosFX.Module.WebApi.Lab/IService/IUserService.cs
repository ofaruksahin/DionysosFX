using System.Collections.Generic;

namespace DionysosFX.Module.WebApi.Lab
{
    public interface IUserService
    {
        void Initialize();
        List<User> GetAll();
        User Get(int id);
        int Insert(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}
