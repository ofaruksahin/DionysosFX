using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    /// <summary>
    /// Controller, Action and Entity add description with this attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method | AttributeTargets.Property,AllowMultiple =false)]
    public class DescriptionAttribute :Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(string Description)
        {
            this.Description = Description;
        }
    }
}
