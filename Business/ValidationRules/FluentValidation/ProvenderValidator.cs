using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProvenderValidator : AbstractValidator<Provender>
    {
        public ProvenderValidator()
        {
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price needs to be greater than zero!");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Price cannot be empty!");
            RuleFor(p => p.Weight).GreaterThan(0).WithMessage("Weight needs to be greater than zero!");
            RuleFor(p => p.Weight).NotEmpty().WithMessage("Weight cannot be empty!");
        }
    }
}