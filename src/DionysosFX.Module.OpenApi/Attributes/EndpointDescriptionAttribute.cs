using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EndpointDescriptionAttribute :Attribute
    {
        public string Description { get; set; }

        public EndpointDescriptionAttribute(string Description)
        {
            this.Description = Description;
        }
    }
}
