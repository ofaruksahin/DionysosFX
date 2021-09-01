using System;

namespace DionysosFX.Swan.DataAnnotations
{
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Method |
        AttributeTargets.Property | 
        AttributeTargets.Field,AllowMultiple =false)]
    public class NotMappedAttribute : Attribute
    {
    }
}
