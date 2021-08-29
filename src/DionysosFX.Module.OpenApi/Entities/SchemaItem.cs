using Newtonsoft.Json;
using System.Collections.Generic;

namespace DionysosFX.Module.OpenApi.Entities
{
    public class SchemaItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("schema_properties")]
        public List<SchemaPropertyItem> SchemaProperties { get; set; }

        public SchemaItem(string Name)
        {
            this.Name = Name;
        }

        public class SchemaPropertyItem
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; } = string.Empty;
        }
    }
}
