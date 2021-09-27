using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    /// <summary>
    /// This attribute add Action Method
    /// Action Method parameters add desciption
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = true)]
    public class ParameterAttribute : Attribute
    {
        public string Name  { get; set; }
        public string Description { get; set; }

        public ParameterAttribute(string Name) 
        {
            this.Name = Name;
        }

        public ParameterAttribute(string Name,string Description)
        {
            this.Name = Name;
            this.Description = this.Description;
        }
    }
}
