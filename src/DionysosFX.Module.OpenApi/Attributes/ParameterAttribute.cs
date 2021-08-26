using DionysosFX.Module.OpenApi.Entities;
using DionysosFX.Swan.Extensions;
using Newtonsoft.Json;
using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    public class ParameterAttribute : ParameterItem
    {
        [JsonIgnore]
        public Type Type { get; set; }

        [JsonIgnore]
        public Type PrefixType { get; set; }

        public ParameterAttribute(string Name, Type Type,Type PrefixType) : base(Name, Type.GetName(),PrefixType.GetName())
        {
            this.Type = Type;
            this.PrefixType = PrefixType;
        }

        public ParameterAttribute(string Name,Type Type,Type PrefixType,string Description) : base(Name,Type.GetName(),PrefixType.GetName(),Description)
        {
            this.Type = Type;
            this.PrefixType = PrefixType;
        }
    }
}
