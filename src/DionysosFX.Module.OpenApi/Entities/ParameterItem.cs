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
        public string PrefixTypeName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        public ParameterItem(string Name,string TypeName,string PrefixTypeName)
        {
            this.Name = Name;            
            this.TypeName = TypeName;
            this.PrefixTypeName = PrefixTypeName;
        }

        public ParameterItem(string Name, string TypeName,string PrefixTypeName, string Description)
        {
            this.Name = Name;
            this.TypeName = TypeName;
            this.PrefixTypeName = PrefixTypeName;
            this.Description = Description;
        }


        [JsonIgnore]
        public override object TypeId => base.TypeId;
    }
}
