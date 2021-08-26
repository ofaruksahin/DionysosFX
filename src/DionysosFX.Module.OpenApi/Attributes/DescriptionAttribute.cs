using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    public class DescriptionAttribute :Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(string Description)
        {
            this.Description = Description;
        }
    }
}
