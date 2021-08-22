using DionysosFX.Swan.Net;
using FluentValidation.Results;
using System;

namespace DionysosFX.Module.FluentValidator
{
    public class OnValidationFailEventArgs : EventArgs
    {
        public ValidationResult ValidationResult
        {
            get;
            set;
        }

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
