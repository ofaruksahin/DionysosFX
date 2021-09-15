using System;

namespace DionysosFX.Swan.DataAnnotations
{
    /// <summary>
    /// Used for not proccessing
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Method |
        AttributeTargets.Property | 
        AttributeTargets.Field,AllowMultiple =false)]    
    public class NotMappedAttribute : Attribute
    {
    }
}
