using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
       public CustomerValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Id cannot be empty!");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Id cannot be empty!");
        }
    }
}