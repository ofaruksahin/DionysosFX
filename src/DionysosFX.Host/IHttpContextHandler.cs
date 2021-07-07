using DionysosFX.Swan.Net;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpContextHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task HandleContextAsync(IHttpContextImpl context);
    }
}
