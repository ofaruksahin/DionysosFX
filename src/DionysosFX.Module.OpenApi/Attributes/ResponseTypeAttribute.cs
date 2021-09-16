using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Swan.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;

namespace DionysosFX.Module.OpenApi.Attributes
{
    /// <summary>
    /// This attribute add action and return response
    /// </summary>
    public class ResponseTypeAttribute : ResponseTypeItem
    {
        [JsonIgnore]
        public Type Type { get; set; }

        public ResponseTypeAttribute(HttpStatusCode StatusCode, Type Type) : base(StatusCode,Type.GetName())
        {
            this.Type = Type;
        }

        public ResponseTypeAttribute(HttpStatusCode StatusCode, Type Type, string Description) : base(StatusCode, Type.GetName(), Description)
        {
            this.Type = Type;
        }
    }
}
