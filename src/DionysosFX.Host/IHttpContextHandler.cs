using System.Threading.Tasks;

namespace DionysosFX.Host
{
    public interface IHttpContextHandler
    {
        Task HandleContextAsync(IHttpContextImpl context);
    }
}
