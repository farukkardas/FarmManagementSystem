using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MilkSalesValidator : AbstractValidator<MilkSales>
    {
        public MilkSalesValidator()
        {
            RuleFor(m => m.Amount).GreaterThan(0).WithMessage("Amount needs to be greater than zero!");
            RuleFor(m => m.Amount).NotEmpty().WithMessage("Amount cannot to be empty!");
            RuleFor(m => m.SalePrice).NotEmpty().WithMessage("Sale Price cannot to be empty!");
            RuleFor(m => m.SalePrice).GreaterThan(0).WithMessage("Sale price needs to be greater than zero!");
        }
    }
}