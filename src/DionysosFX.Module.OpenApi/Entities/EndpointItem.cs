using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class EndpointItem
    {
        /// <summary>
        /// Endpoint Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Endpoint Verb
        /// </summary>
        [JsonProperty("verb")]
        public string Verb { get; set; }

        /// <summary>
        /// Endpoint Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Endpoint parameters
        /// </summary>
        [JsonProperty("parameters")]
        public List<ParameterItem> Parameters { get; set; } = new List<ParameterItem>();

        /// <summary>
        /// Endpoint response types
        /// </summary>
        [JsonProperty("response_types")]
        public List<ResponseTypeItem> ResponseTypes { get; set; } = new List<ResponseTypeItem>();

        /// <summary>
        /// Endpoint versions
        /// </summary>
        [JsonProperty("versions")]
        public List<VersionItem> Versions { get; set; } = new List<VersionItem>();
    }
}
