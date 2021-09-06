using DionysosFX.Module.IWebApi;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Reflection;

namespace DionysosFX.Module.WebApi.XML
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class XmlDataAttribute : Attribute, IParameterConverter
    {
        public object Convert(IHttpContext context, RouteResolveResponse route, ParameterInfo parameterInfo)
        {
            return new { };
        }
    }
}
