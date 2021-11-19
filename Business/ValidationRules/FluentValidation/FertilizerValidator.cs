using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FertilizerValidator : AbstractValidator<Fertilizer>
    {
        public FertilizerValidator()
        {
             RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price needs to be greater than zero!");
             RuleFor(c => c.Weight).GreaterThan(0).WithMessage("Price needs to be greater than zero!");
        }
        
    }
}