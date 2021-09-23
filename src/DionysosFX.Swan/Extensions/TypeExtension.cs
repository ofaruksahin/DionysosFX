using Autofac;
using DionysosFX.Swan.DataAnnotations;
using DionysosFX.Swan.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DionysosFX.Swan.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// Type get method via reflection
        /// </summary>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Parameter is type
        /// </summary>
        /// <param name="parameterInfo"></param>
        /// <returns></returns>
        public static bool IsArray(this ParameterInfo parameterInfo) => (parameterInfo.ParameterType.IsArray) || (parameterInfo.ParameterType.IsGenericType && parameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(List<>));

        /// <summary>
        /// Property is type
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsArray(this PropertyInfo propertyInfo) => (propertyInfo.PropertyType.IsArray) || (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>));

        /// <summary>
        /// Convert object to web form data
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Type get attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> GetAttributes<T>(this Type type) => type
            .GetCustomAttributes()
            .Where(f => f.GetType() == typeof(T))
            .Select(f => (T)Convert.ChangeType(f, typeof(T)))
            .ToList();

        /// <summary>
        /// Method get attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static List<T> GetAttributes<T>(this MethodInfo methodInfo) =>
            methodInfo
            .GetCustomAttributes()
            .Where(f => f.GetType() == typeof(T))
            .Select(f => (T)Convert.ChangeType(f, typeof(T)))
            .ToList();

        /// <summary>
        /// Object get attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> GetAttributes<T>(this object obj) =>
            obj
            .GetType()
            .GetCustomAttributes()
            .Where(f => f.GetType() == typeof(T))
            .Select(f => (T)Convert.ChangeType(f, typeof(T)))
            .ToList();

        /// <summary>
        /// Type is system type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSystemType(this Type type) => type.Assembly == typeof(object).Assembly;

        /// <summary>
        /// This method get type properties with recursive
        /// </summary>
        /// <param name="type"></param>
        /// <param name="types"></param>
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

        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="bindingFlags"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object? Invoke(this object instance,string methodName,BindingFlags bindingFlags, object[]? parameters)
        {
            var methodInfo = instance.GetType().GetCustomMethod(methodName, bindingFlags);
            if (methodInfo == null)
                return null;
            return methodInfo.Invoke(instance, parameters);
        }

        public static object CreateInstance<T>(this List<ParameterInfo> constructorParameters, IContainer container) => constructorParameters.CreateInstance(container, typeof(T));

        public static object CreateInstance(this List<ParameterInfo> constructorParameters, IContainer container,Type destType)
        {
            object instance = null;
            List<object> _parameters = new List<object>();

            foreach (var item in constructorParameters)
            {
                object ctParam = null;
                if (!container.TryResolve(item.ParameterType, out ctParam))
                    _parameters.Add(null);
                else
                    _parameters.Add(ctParam);
            }

            try
            {
                instance = Activator.CreateInstance(destType, _parameters.ToArray());
            }
            catch (Exception)
            {

            }
            return instance;
        }

        public static bool IsNotMapped(this Type @this) => @this.GetCustomAttribute<NotMappedAttribute>() != null;

        public static bool IsNotMapped(this MethodInfo @this) => @this.GetCustomAttribute<NotMappedAttribute>() != null;
    }
}
