using DionysosFX.Module.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.Template.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IController<TEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        void List();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Get([QueryData]int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert([JsonData]TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        void Update([QueryData] int id, [JsonData] TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete([QueryData] int id);
    }
}
