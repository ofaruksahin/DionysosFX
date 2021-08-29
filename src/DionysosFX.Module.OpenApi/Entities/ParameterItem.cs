using Newtonsoft.Json;
using System;

namespace DionysosFX.Module.OpenApi.Entities
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterItem : Attribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string TypeName { get; set; }

        [JsonProperty("prefix_type")]
        public string PrefixType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        public ParameterItem(string Name)
        {
            this.Name = Name;           
        }

        public ParameterItem(string Name,string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }


        [JsonIgnore]
        public override object TypeId => base.TypeId;
    }
}
