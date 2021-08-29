using DionysosFX.Module.OpenApi.Entities;

namespace DionysosFX.Module.OpenApi.Attributes
{
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
