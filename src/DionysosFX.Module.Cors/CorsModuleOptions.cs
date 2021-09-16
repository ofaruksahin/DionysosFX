using System.Collections.Generic;

namespace DionysosFX.Module.Cors
{
    /// <summary>
    /// Cors module options
    /// </summary>
    public class CorsModuleOptions
    {
        /// <summary>
        /// Allowed request headers
        /// </summary>
        internal List<string> AllowedHeaders { get; set; } = new List<string>();
        /// <summary>
        /// Allowed request methods
        /// </summary>
        internal List<string> AllowedMethods { get; set; } = new List<string>();
        /// <summary>
        /// Allowed request origins
        /// </summary>
        internal List<string> AllowedOrigins { get; set; } = new List<string>();
        /// <summary>
        /// Cache timeout
        /// </summary>
        public double MaxAge { get; set; } = 1800;
    }
}
