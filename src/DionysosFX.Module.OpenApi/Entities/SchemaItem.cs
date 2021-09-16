using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    internal class SchemaItem
    {
        /// <summary>
        /// Schema Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Schema description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Schema properties
        /// </summary>
        [JsonProperty("schema_properties")]
        public List<SchemaPropertyItem> SchemaProperties { get; set; }

        public SchemaItem(string Name)
        {
            this.Name = Name;
        }

        public class SchemaPropertyItem
        {
            /// <summary>
            /// Property name
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Property type
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// Property description
            /// </summary>
            [JsonProperty("description")]
            public string Description { get; set; } = string.Empty;
        }
    }
}
