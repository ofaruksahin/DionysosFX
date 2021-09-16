using DionysosFX.Swan.Net;
using FluentValidation.Results;
using System;

namespace DionysosFX.Module.FluentValidator
{
    /// <summary>
    /// 'OnValidationFail' event args
    /// </summary>
    public class OnValidationFailEventArgs : EventArgs
    {
        /// <summary>
        /// Object validation after then return ValidationResult
        /// </summary>
        public ValidationResult ValidationResult
        {
            get;
            set;
        }

        /// <summary>
        /// Context
        /// </summary>
        public IHttpContext Context
        {
            get;
            set;
        }

        public OnValidationFailEventArgs(ValidationResult validationResult,IHttpContext context)
        {
            ValidationResult = validationResult;
            Context = context;
        }
    }
}
