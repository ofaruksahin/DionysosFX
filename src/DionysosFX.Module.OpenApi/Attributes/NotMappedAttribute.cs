using System;

namespace DionysosFX.Module.OpenApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =false)]
    public class NotMappedAttribute : Attribute
    {
    }
}
