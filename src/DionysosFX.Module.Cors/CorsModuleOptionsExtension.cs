namespace DionysosFX.Module.Cors
{
    public static class CorsModuleOptionsExtension
    {

        public static CorsModuleOptions AllowAnyOrigin(this CorsModuleOptions @this)
        {
            @this.AllowedOrigins.Add("*");
            return @this;
        }

        public static CorsModuleOptions AllowAnyMethods(this CorsModuleOptions @this)
        {
            @this.AllowedMethods.Add("*");
            return @this;
        }

        public static CorsModuleOptions AllowAnyHeaders(this CorsModuleOptions @this)
        {
            @this.AllowedHeaders.Add("*");
            return @this;
        }

        public static CorsModuleOptions WithOrigins(this CorsModuleOptions @this,params string[] origins)
        {
            @this.AllowedOrigins.AddRange(origins);
            return @this;
        }

        public static CorsModuleOptions WithMethods(this CorsModuleOptions @this,params string[] methods)
        {
            @this.AllowedMethods.AddRange(methods);
            return @this;
        }

        public static CorsModuleOptions WithHeaders(this CorsModuleOptions @this,params string[] headers)
        {
            @this.AllowedHeaders.AddRange(headers);
            return @this;
        }
    }
}
