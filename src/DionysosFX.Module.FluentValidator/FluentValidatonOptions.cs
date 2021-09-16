using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DionysosFX.Module.FluentValidator
{
    /// <summary>
    /// FluentValidationOptions
    /// </summary>
    public class FluentValidatonOptions
    {
        /// <summary>
        /// This variable store type and type validator
        /// </summary>
        IDictionary<Type, Type> validators = new Dictionary<Type, Type>();

        /// <summary>
        /// This variable provide auto detect validator object
        /// </summary>
        private bool _autoDetect;

        /// <summary>
        /// This variable provide auto detect validator object
        /// </summary>
        public bool AutoDetect
        {
            get => _autoDetect;
            set
            {
                _autoDetect = true;
                validators.Clear();
                if (_autoDetect)
                    AutoDetectValidators();
            }
        }

        /// <summary>
        /// Object validate failed then trigged event
        /// </summary>
        public event EventHandler<OnValidationFailEventArgs> OnValidationFail;

        public FluentValidatonOptions()
        {
            AutoDetect = true;
        }

        /// <summary>
        /// 'AutoDetect' set true and auto detect validators object
        /// </summary>
        private void AutoDetectValidators()
        {
            validators.Clear();
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

        /// <summary>
        /// Validators variable add type and validators
        /// </summary>
        /// <param name="type"></param>
        /// <param name="validator"></param>
        public void AddValidator(Type type, Type validator) => validators.TryAdd(type, validator);

        /// <summary>
        /// Have type is validators?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsValidatable(Type type)
        {
            return validators.TryGetValue(type, out _);
        }

        /// <summary>
        /// Object validate
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Trigged OnValidationFail event
        /// </summary>
        /// <param name="eventArgs"></param>
        private void TriggerOnValidationFail(OnValidationFailEventArgs eventArgs)
        {
            OnValidationFail?.Invoke(this, eventArgs);
        }
    }
}
