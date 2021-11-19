using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FuelValidator : AbstractValidator<FuelConsumption>
    {
        public FuelValidator()
        {
            RuleFor(f => f.Price).GreaterThan(0).WithMessage("Price needs to be greater than zero!");
            RuleFor(f => f.BoughtDate).NotEmpty().WithMessage("Bought date cannot be empty!");
        }
    }
}