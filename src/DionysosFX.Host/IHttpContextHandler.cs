using DionysosFX.Swan.Net;
using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public interface IHttpContextHandler
    {
        /// <summary>
        /// Wait a new web request and process request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task HandleContextAsync(IHttpContextImpl context);
    }
}
