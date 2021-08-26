using System;
using System.Linq;

namespace DionysosFX.Swan.Extensions
{
    public static class StringExtension
    {
        public static string GetName(this Type type)
        {
            string text = string.Empty;
            if (!type.IsGenericType)
            {
                text = type.FullName;
            }
            else
            {
                var typeName = type.Name;
                typeName = $"{typeName.Substring(0, typeName.IndexOf('`'))}<";
                typeName += string.Join(",",type.GetGenericArguments().Select(f=>f.GetName()));
                typeName += ">";
                text = typeName;
            }
            return text;
        }
    }
}
