using DionysosFX.Module.WebApi.Attributes;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace DionysosFX.Module.WebApi
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class JsonDataAttribute : Attribute, IParameterConverter
    {
        public object Convert(IHttpContext context, RouteResolveResponse route, ParameterInfo parameterInfo)
        {
            string body = string.Empty;
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                body = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(body))
                return null;

            return JsonConvert.DeserializeObject(body, parameterInfo.ParameterType);
        }
    }
}
