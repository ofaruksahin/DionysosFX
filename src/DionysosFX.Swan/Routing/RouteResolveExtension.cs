using System.Reflection;

namespace DionysosFX.Swan.Routing
{
    public static class RouteResolveExtension
    {
        public static bool IsRoute(this MethodInfo @this)
        {
            var attribute = @this.GetCustomAttribute(typeof(RouteAttribute));
            if (attribute != null)
            {
                return true;
            }
            return false;
        }
    }
}
