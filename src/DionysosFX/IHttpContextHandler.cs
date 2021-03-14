using System.Threading.Tasks;

namespace DionysosFX
{
    public interface IHttpContextHandler
    {
        Task HandleContextAsync(IHttpContextImpl context);
    }
}
