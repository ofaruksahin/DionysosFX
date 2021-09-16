using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class ControllerItem
    {
        /// <summary>
        /// Controller Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Controller Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Controller endpoints
        /// </summary>
        [JsonProperty("endpoints")]
        public List<EndpointItem> Endpoints { get; set; } = new List<EndpointItem>();
    }
}
