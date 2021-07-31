using System;
using System.Net;

namespace DionysosFX.Module.OpenApi
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = true)]
    public class ResponseTypeAttribute : Attribute
    {
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        } = string.Empty;

        public ResponseTypeAttribute(HttpStatusCode statusCode,Type type)
        {
            StatusCode = statusCode;
            Type = type;
        }

        public ResponseTypeAttribute(HttpStatusCode statusCode,Type type,string description)
        {
            StatusCode = statusCode;
            Type = type;
            Description = description;
        }
    }
}
