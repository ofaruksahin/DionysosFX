using Newtonsoft.Json;
using System;
using System.Net;

namespace DionysosFX.Module.OpenApi.Entities
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ResponseTypeItem  :Attribute
    {
        /// <summary>
        /// Response status code
        /// </summary>
        [JsonProperty("status_code")]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Response data type
        /// </summary>
        [JsonProperty("type")]
        public string TypeName { get; set; }

        /// <summary>
        /// Response description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        public ResponseTypeItem(HttpStatusCode StatusCode,string TypeName)
        {
            this.StatusCode = StatusCode;
            this.TypeName = TypeName;            
        }

        public ResponseTypeItem(HttpStatusCode StatusCode, string TypeName,string Description)
        {
            this.StatusCode = StatusCode;
            this.TypeName = TypeName;
            this.Description = Description;
        }

        [JsonIgnore]
        public override object TypeId => base.TypeId;
    }
}
