using DionysosFX.Module.WebApi;
using DionysosFX.Module.WebApi.EnpointResults;
using DionysosFX.Swan.Routing;
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
        [Route(HttpVerb.GET, "/list")]
        IEndpointResult List();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.GET,"/get")]
        IEndpointResult Get([QueryData] int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        [Route(HttpVerb.POST,"/insert")]
        void Insert([JsonData] TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        [Route(HttpVerb.PUT,"/update")]
        void Update([QueryData] int id, [JsonData] TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [Route(HttpVerb.DELETE,"/delete")]
        void Delete([QueryData] int id);
    }
}
