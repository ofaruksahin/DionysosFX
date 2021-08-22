using DionysosFX.Module.FluentValidator.Lab.Entities;
using FluentValidation;

namespace DionysosFX.Module.FluentValidator.Lab.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.Surname).NotEmpty();
            RuleFor(f => f.Age).GreaterThanOrEqualTo(18);
        }
    }
}
