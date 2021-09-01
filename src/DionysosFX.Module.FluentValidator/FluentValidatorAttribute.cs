using Autofac;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using FluentValidation.Results;
using System;
using System.Reflection;

namespace DionysosFX.Module.FluentValidator
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class FluentValidatorAttribute : Attribute, IEndpointFilter
    {
        public void OnBefore(object sender, IHttpContext httpContext)
        {
            if(!httpContext.Container.TryResolve(out FluentValidatonOptions validationContainer))            
                throw new ArgumentNullException(nameof(validationContainer), $"{nameof(validationContainer)} is null, you should use AddFluentValidatorModule method");
            if (sender is RouteResolveResponse rsv)
            {
                var isValidatableMethod = validationContainer.GetType().GetMethod(FluentValidatorConstants.IsValidatable, BindingFlags.Instance | BindingFlags.NonPublic);
                var validateMethod = validationContainer.GetType().GetMethod(FluentValidatorConstants.ValidateMethod, BindingFlags.Instance | BindingFlags.NonPublic);
                var triggerOnValidationFail = validationContainer.GetType().GetMethod(FluentValidatorConstants.TriggerOnValidationFail, BindingFlags.Instance | BindingFlags.NonPublic);
                if (isValidatableMethod == null)
                    throw new Exception("FluentValidatorModule IsValidatable method not found");
                if (validateMethod == null)
                    throw new Exception("FluentValidatorModule Validate method not found");
                if (triggerOnValidationFail == null)
                    throw new Exception("FluentValidatorModule TriggerOnValidationFail method not found");
                foreach (var parameter in rsv.Parameters)
                {
                    var parameterType = parameter.GetType();
                    if ((bool)isValidatableMethod.Invoke(validationContainer, new object[] { parameterType }))
                    {
                        var validateResult = (ValidationResult)validateMethod.Invoke(validationContainer, new object[] { parameter });
                        if (validateResult != null && !validateResult.IsValid)
                            triggerOnValidationFail.Invoke(validationContainer, new object[] { new OnValidationFailEventArgs(validateResult, httpContext) });
                    }
                }
            }
        }


        public void OnAfter(object sender, IHttpContext httpContext)
        {
        }
    }
}
