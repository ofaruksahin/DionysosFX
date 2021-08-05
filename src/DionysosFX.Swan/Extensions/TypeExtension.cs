using System;
using System.Collections.Generic;
using System.Reflection;

namespace DionysosFX.Swan.Extensions
{
    public static class TypeExtension
    {
        public static MethodInfo GetCustomMethod(this Type @this, string methodName, BindingFlags flags)
        {
            MethodInfo methodInfo = null;
            Type baseType = @this;

            methodInfo = @this.GetMethod(methodName, flags);

            if (methodInfo == null)
            {
                baseType = null;
                do
                {
                    if (baseType == null && @this.BaseType != null)
                        baseType = @this.BaseType;
                    else if (baseType != null && baseType.BaseType != null)
                        baseType = baseType.BaseType;
                    else
                        break;
                    methodInfo = baseType.GetMethod(methodName, flags);
                } while (methodInfo == null);
            }

            return methodInfo;
        }    
        
        public static bool IsArray(this ParameterInfo parameterInfo) => (parameterInfo.ParameterType.IsArray) || (parameterInfo.ParameterType.IsGenericType && parameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(List<>));

        public static bool IsArray(this PropertyInfo propertyInfo) => (propertyInfo.PropertyType.IsArray) || (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>));
    }
}
