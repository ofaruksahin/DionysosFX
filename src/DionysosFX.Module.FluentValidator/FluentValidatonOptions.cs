using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Module.FluentValidator
{
    public class FluentValidatonOptions
    {
        IDictionary<Type, Type> validators = new Dictionary<Type, Type>();

        public event EventHandler<OnValidationFailEventArgs> OnValidationFail;

        public FluentValidatonOptions()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var assemblyTypes = assembly
                    .GetTypes()
                    .Where(f => f.BaseType != null && f.BaseType.Name == "AbstractValidator`1")
                    .ToList();
                foreach (var assemblyType in assemblyTypes)
                {
                    var entity = assemblyType.BaseType.GenericTypeArguments[0];
                    validators.TryAdd(entity, assemblyType);
                }
            }
        }

        private bool IsValidatable(Type type)
        {
            return validators.TryGetValue(type, out _);
        }

        private ValidationResult Validate(object obj)
        {
            if (validators.TryGetValue(obj.GetType(), out Type validator))
            {
                var instance = Activator.CreateInstance(validator);
                var validateMethod = instance.GetType().GetMethods().FirstOrDefault(f => f.Name == FluentValidatorConstants.ValidateMethod);
                if (validateMethod != null)
                    return (validateMethod.Invoke(instance, new object[] { obj }) is ValidationResult res) ? res : null;
            }
            return null;
        }        

        private void TriggerOnValidationFail(OnValidationFailEventArgs eventArgs)
        {
            OnValidationFail?.Invoke(this, eventArgs);
        }
    }
}
