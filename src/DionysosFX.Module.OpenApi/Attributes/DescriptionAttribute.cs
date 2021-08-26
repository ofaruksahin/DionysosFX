using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =false)]
    public class DescriptionAttribute :Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(string Description)
        {
            this.Description = Description;
        }
    }
}
