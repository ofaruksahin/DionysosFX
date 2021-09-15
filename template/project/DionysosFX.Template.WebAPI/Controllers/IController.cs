using DionysosFX.Module.IWebApi;
using DionysosFX.Module.WebApi.JSON;

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
        IEndpointResult List();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        IEndpointResult Get([QueryData] int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        IEndpointResult Insert([JsonData] TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        IEndpointResult Update([QueryData] int id, [JsonData] TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        IEndpointResult Delete([QueryData] int id);
    }
}
