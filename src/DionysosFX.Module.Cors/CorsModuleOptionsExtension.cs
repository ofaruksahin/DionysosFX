using System.Linq;

namespace DionysosFX.Module.Cors
{
    public static class CorsModuleOptionsExtension
    {
        public static CorsPolicy AddPolicy(this CorsModuleOptions corsModuleOptions,string name)
        {
            var corsPolicy = corsModuleOptions.CorsPolicies.FirstOrDefault(f => f.Name == name);
            if (corsPolicy != null)
                return corsPolicy;
            corsPolicy = new CorsPolicy();
            corsPolicy.Name = name;
            corsModuleOptions.CorsPolicies.Add(corsPolicy);
            return corsPolicy;
        }

        public static CorsPolicy AllowAnyOrigin(this CorsPolicy corsPolicy)
        {
            corsPolicy.AllowedOrigins.Add("*");
            return corsPolicy;
        }

        public static CorsPolicy AllowAnyMethods(this CorsPolicy corsPolicy)
        {
            corsPolicy.AllowedMethods.Add("*");
            return corsPolicy;
        }

        public static CorsPolicy AllowAnyHeaders(this CorsPolicy corsPolicy)
        {
            corsPolicy.AllowedHeaders.Add("*");
            return corsPolicy;
        }

        public static CorsPolicy WithOrigins(this CorsPolicy corsPolicy,params string[] origins)
        {
            corsPolicy.AllowedOrigins.AddRange(origins);
            return corsPolicy;
        }

        public static CorsPolicy WithMethods(this CorsPolicy corsPolicy,params string[] methods)
        {
            corsPolicy.AllowedMethods.AddRange(methods);
            return corsPolicy;
        }

        public static CorsPolicy WithHeaders(this CorsPolicy corsPolicy,params string[] headers)
        {
            corsPolicy.AllowedHeaders.AddRange(headers);
            return corsPolicy;
        }
    }
}
