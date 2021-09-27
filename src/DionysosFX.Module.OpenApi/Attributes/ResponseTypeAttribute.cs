using System;
using System.Net;

namespace DionysosFX.Module.OpenApi.Attributes
{
    /// <summary>
    /// This attribute add action and return response
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class ResponseTypeAttribute :Attribute
    {
        public Type Type { get; set; }
        public string Description { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ResponseTypeAttribute(HttpStatusCode StatusCode, Type Type) 
        {
            this.Type = Type;
            this.StatusCode = StatusCode;
        }

        public ResponseTypeAttribute(HttpStatusCode StatusCode, Type Type, string Description)
        {
            this.Type = Type;
            this.StatusCode = StatusCode;
            this.Description = Description;
        }
    }
}
