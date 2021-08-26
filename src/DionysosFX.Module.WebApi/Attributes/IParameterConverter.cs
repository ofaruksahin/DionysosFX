using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System.Reflection;

namespace DionysosFX.Module.WebApi.Attributes
{

    public interface IParameterConverter
    {
        object Convert(IHttpContext context, RouteResolveResponse route, ParameterInfo parameterInfo);
    }
}
