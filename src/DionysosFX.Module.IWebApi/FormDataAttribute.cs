using DionysosFX.Swan.HttpMultipart;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Reflection;

namespace DionysosFX.Module.IWebApi
{
    /// <summary>
    /// Object type is form data
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FormDataAttribute : Attribute, IParameterConverter
    {
        public object Convert(IHttpContext context, RouteResolveResponse route, ParameterInfo parameterInfo)
        {
            return context.ToFormObject(parameterInfo.ParameterType);
        }
    }
}
