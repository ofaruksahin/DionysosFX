using System.Collections.Generic;

namespace DionysosFX.Module.Cors
{
    public class CorsPolicy
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; } = false;
        public List<string> AllowedHeaders { get; set; } = new List<string>();
        public List<string> AllowedMethods { get; set; } = new List<string>();
        public List<string> AllowedOrigins { get; set; } = new List<string>();
        public double MaxAge { get; set; } = 1800;
    }
}
