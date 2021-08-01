using DionysosFX.Swan.Net;
using System;
using System.Reflection;

namespace DionysosFX.Swan.HttpMultipart
{
    public static class MultipartConverter
    {
        public static object ToFormObject(this IHttpContext @this,Type destType)
        {
            var result = Activator.CreateInstance(destType);
            var properties = result.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool isArray = property.PropertyType.IsArray;
                if (isArray)
                {

                }
                else
                {
                    
                }
            }
            return result;
        }
    }
}
