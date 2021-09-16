namespace DionysosFX.Module.Cors
{
    public static class CorsModuleOptionsExtension
    {
        /// <summary>
        /// AllowedOrigins add any origin
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static CorsModuleOptions AllowAnyOrigin(this CorsModuleOptions @this)
        {
            @this.AllowedOrigins.Add("*");
            return @this;
        }

        /// <summary>
        /// AllowedMethods add any method
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static CorsModuleOptions AllowAnyMethods(this CorsModuleOptions @this)
        {
            @this.AllowedMethods.Add("*");
            return @this;
        }

        /// <summary>
        /// Allowed add any headers
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static CorsModuleOptions AllowAnyHeaders(this CorsModuleOptions @this)
        {
            @this.AllowedHeaders.Add("*");
            return @this;
        }

        /// <summary>
        /// Allowed origins add origin
        /// </summary>
        /// <param name="this"></param>
        /// <param name="origins"></param>
        /// <returns></returns>
        public static CorsModuleOptions WithOrigins(this CorsModuleOptions @this,params string[] origins)
        {
            @this.AllowedOrigins.AddRange(origins);
            return @this;
        }

        /// <summary>
        /// Allowed methods add method
        /// </summary>
        /// <param name="this"></param>
        /// <param name="methods"></param>
        /// <returns></returns>
        public static CorsModuleOptions WithMethods(this CorsModuleOptions @this,params string[] methods)
        {
            @this.AllowedMethods.AddRange(methods);
            return @this;
        }

        /// <summary>
        /// Allowed methods add headers
        /// </summary>
        /// <param name="this"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static CorsModuleOptions WithHeaders(this CorsModuleOptions @this,params string[] headers)
        {
            @this.AllowedHeaders.AddRange(headers);
            return @this;
        }
    }
}
