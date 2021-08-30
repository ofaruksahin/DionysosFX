using System.Collections.Generic;

namespace DionysosFX.Module.Cors
{
    public class CorsModuleOptions
    {
        public List<CorsPolicy> CorsPolicies { get; set; } = new List<CorsPolicy>();
    }
}
