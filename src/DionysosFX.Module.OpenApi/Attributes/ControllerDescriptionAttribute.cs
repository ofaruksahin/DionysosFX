using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class ControllerDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public ControllerDescriptionAttribute(string Description)
        {
            this.Description = Description;
        }
    }
}
