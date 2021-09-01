using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class EndpointItem
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("verb")]
        public string Verb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("parameters")]
        public List<ParameterItem> Parameters { get; set; } = new List<ParameterItem>();

        [JsonProperty("response_types")]
        public List<ResponseTypeItem> ResponseTypes { get; set; } = new List<ResponseTypeItem>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("versions")]
        public List<VersionItem> Versions { get; set; } = new List<VersionItem>();
    }
}
