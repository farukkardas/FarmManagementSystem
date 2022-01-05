using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CcValidator :AbstractValidator<CreditCart>
    {
       public CcValidator()
       {
           // This a demo validator for credit cart
           RuleFor(x => x.CartNumber).CreditCard().WithMessage("This is a not valid credit cart!");
           RuleFor(x => x.CvvNumber).NotEmpty().NotNull().WithMessage("CVV number is not correct!");
           RuleFor(x=>x.ExpirationDate).MinimumLength(3).NotEmpty().NotNull().WithMessage("Expiration Date is not correct!");
           RuleFor(x => x.FullName).NotEmpty().NotNull().MinimumLength(5).Matches("[a-zA-Z]").WithMessage("Name is not correct!");
       }
    }
}