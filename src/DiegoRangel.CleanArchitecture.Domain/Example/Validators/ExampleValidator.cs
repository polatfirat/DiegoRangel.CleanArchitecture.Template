using FluentValidation;

namespace DiegoRangel.CleanArchitecture.Domain.Example.Validators
{
    public class ExampleValidator : AbstractValidator<Example>
    {
        public ExampleValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}