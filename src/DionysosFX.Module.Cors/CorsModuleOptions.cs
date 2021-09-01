using System.Collections.Generic;

namespace DionysosFX.Module.Cors
{
    public class CorsModuleOptions
    {
        internal List<string> AllowedHeaders { get; set; } = new List<string>();
        internal List<string> AllowedMethods { get; set; } = new List<string>();
        internal List<string> AllowedOrigins { get; set; } = new List<string>();
        internal double MaxAge { get; set; } = 1800;
    }
}
