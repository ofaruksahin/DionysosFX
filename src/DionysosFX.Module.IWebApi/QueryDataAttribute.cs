using DionysosFX.Swan.Extensions;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Module.IWebApi
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class QueryDataAttribute :Attribute, IParameterConverter
    {
        public object Convert(IHttpContext context, RouteResolveResponse route, ParameterInfo parameterInfo)
        {
            if (!route.QueryString.Any(f => f == parameterInfo.Name))
                return null;
            bool isArray = parameterInfo.IsArray();
            if (isArray)
            {
                if (parameterInfo.ParameterType.GenericTypeArguments.Any())
                    return context.Request.QueryString[parameterInfo.Name].Split(',').Select(f => f).ToList();
                else
                    return context.Request.QueryString[parameterInfo.Name].Split(',').Select(f => f).ToArray();
            }
            else
            {
                return System.Convert.ChangeType(context.Request.QueryString[parameterInfo.Name], parameterInfo.ParameterType);
            }
        }
    }
}
