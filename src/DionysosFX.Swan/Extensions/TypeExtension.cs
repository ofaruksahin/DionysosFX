using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static List<(string, string)> ToFormData(this object @this)
        {
            List<(string, string)> result = new List<(string, string)>();
            var properties = @this.GetType().GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(@this);
                if (property.IsArray())
                {
                    foreach (var item in (IEnumerable)value)
                        result.Add((name, item.ToString()));
                }
                else
                {
                    result.Add((name, value.ToString()));
                }
            }
            return result;
        }

        public static List<T> GetAttributes<T>(this Type type) => type
            .GetCustomAttributes()
            .Where(f => f.GetType() == typeof(T))
            .Select(f => (T)Convert.ChangeType(f, typeof(T)))
            .ToList();

        public static List<T> GetAttributes<T>(this MethodInfo methodInfo) =>
            methodInfo
            .GetCustomAttributes()
            .Where(f => f.GetType() == typeof(T))
            .Select(f => (T)Convert.ChangeType(f, typeof(T)))
            .ToList();

        public static List<T> GetAttributes<T>(this object obj) =>
            obj
            .GetType()
            .GetCustomAttributes()
            .Where(f => f.GetType() == typeof(T))
            .Select(f => (T)Convert.ChangeType(f, typeof(T)))
            .ToList();

        public static bool IsSystemType(this Type type) => type.Assembly == typeof(object).Assembly;

        public static void GetGenericTypesRecursive(this Type type, ref List<Type> types)
        {
            var genericArguments = type.GetGenericArguments();
            if (genericArguments.Any())
            {
                foreach (var genericArgument in genericArguments)
                    genericArgument.GetGenericTypesRecursive(ref types);
            }
            else
            {
                if (!types.Any(f => f.FullName == type.FullName))
                    types.Add(type);
            }
        }

        public static object? Invoke(this object instance,string methodName,BindingFlags bindingFlags, object[]? parameters)
        {
            var methodInfo = instance.GetType().GetMethod(methodName, bindingFlags);
            if (methodInfo == null)
                return null;
            return methodInfo.Invoke(instance, parameters);
        }
    }
}
