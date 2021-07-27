using DionysosFX.Template.WebAPI.Entities;
using DionysosFX.Template.WebAPI.IService;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Template.WebAPI.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        List<User> list = new List<User>()
        {
            new User
            {
                Id = 1,
                Name = "Ömer Faruk",
                Surname = "Şahin"
            }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(User entity)=> list.Remove(entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(int id)=> list.FirstOrDefault(f => f.Id == id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<User> GetAll() => list.ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(User entity)
        {
            list.Add(entity);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(int id, User entity)
        {
            var user = list.FirstOrDefault(f => f.Id == id);
            if (user == null)
                return false;

            user = entity;
            return true;
        }
    }
}
