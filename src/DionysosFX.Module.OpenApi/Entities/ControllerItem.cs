using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class ControllerItem
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("endpoints")]
        public List<EndpointItem> Endpoints { get; set; } = new List<EndpointItem>();
    }
}
