using Newtonsoft.Json;
using System;

namespace DionysosFX.Module.OpenApi.Entities
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterItem : Attribute
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Parameter Type Name
        /// </summary>
        [JsonProperty("type")]
        public string TypeName { get; set; }

        /// <summary>
        /// Parameter Converter Type 'Query', 'Form', 'Json'
        /// </summary>
        [JsonProperty("prefix_type")]
        public string PrefixType { get; set; }

        /// <summary>
        /// Parameter description
        /// </summary>
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

        /// <summary>
        /// Parameter type id
        /// </summary>
        [JsonIgnore]
        public override object TypeId => base.TypeId;
    }
}
