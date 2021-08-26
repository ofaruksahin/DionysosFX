using DionysosFX.Module.WebApi.Attributes;
using DionysosFX.Swan.HttpMultipart;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using System;
using System.Reflection;

namespace DionysosFX.Module.WebApi
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FormDataAttribute : Attribute, IParameterConverter
    {
        public object Convert(IHttpContext context, RouteResolveResponse route, ParameterInfo parameterInfo) => context.ToFormObject(parameterInfo.ParameterType);
    }
}
