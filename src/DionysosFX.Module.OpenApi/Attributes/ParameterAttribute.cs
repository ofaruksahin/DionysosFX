using DionysosFX.Module.OpenApi.Entities;

namespace DionysosFX.Module.OpenApi.Attributes
{
    /// <summary>
    /// This attribute add Action Method
    /// Action Method parameters add desciption
    /// </summary>
    public class ParameterAttribute : ParameterItem
    {

        public ParameterAttribute(string Name) : base(Name)
        {
        }

        public ParameterAttribute(string Name,string Description) : base(Name,Description)
        {
        }
    }
}
