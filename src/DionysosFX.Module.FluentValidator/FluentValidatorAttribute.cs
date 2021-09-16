using Autofac;
using DionysosFX.Swan.Exceptions;
using DionysosFX.Swan.Net;
using DionysosFX.Swan.Routing;
using FluentValidation.Results;
using System;
using System.Reflection;

namespace DionysosFX.Module.FluentValidator
{
    /// <summary>
    /// This EndpointFilter handle request before validate method
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class FluentValidatorAttribute : Attribute, IEndpointFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="httpContext"></param>
        public void OnBefore(object sender, IHttpContext httpContext)
        {
            if (!httpContext.Container.TryResolve(out FluentValidatonOptions validationContainer))
                throw new ArgumentNullException(nameof(validationContainer), $"{nameof(validationContainer)} is null, you should use AddFluentValidatorModule method");
            if (sender is RouteResolveResponse rsv)
            {
                var isValidatableMethod = validationContainer.GetType().GetMethod(FluentValidatorConstants.IsValidatable, BindingFlags.Instance | BindingFlags.NonPublic);
                var validateMethod = validationContainer.GetType().GetMethod(FluentValidatorConstants.ValidateMethod, BindingFlags.Instance | BindingFlags.NonPublic);
                var triggerOnValidationFail = validationContainer.GetType().GetMethod(FluentValidatorConstants.TriggerOnValidationFail, BindingFlags.Instance | BindingFlags.NonPublic);
                if (isValidatableMethod == null)
                    throw new MethodNotFoundException(nameof(FluentValidatorConstants.IsValidatable));
                if (validateMethod == null)
                    throw new MethodNotFoundException(nameof(FluentValidatorConstants.ValidateMethod));
                if (triggerOnValidationFail == null)
                    throw new MethodNotFoundException(nameof(FluentValidatorConstants.TriggerOnValidationFail));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="httpContext"></param>
        public void OnAfter(object sender, IHttpContext httpContext)
        {
        }
    }
}
