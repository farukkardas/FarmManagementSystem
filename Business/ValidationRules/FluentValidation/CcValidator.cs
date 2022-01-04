using System;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.Validators;

namespace Business.ValidationRules.FluentValidation
{
    public class CcValidator :AbstractValidator<CreditCart>
    {
       public CcValidator()
       {
           // This a demo validator for credit cart
           RuleFor(x => x.CartNumber).CreditCard().WithMessage("This is a not valid credit cart!");
           RuleFor(x => x.CvvNumber).NotEmpty().NotNull().GreaterThan(2).WithMessage("CVV number is not correct!");
           RuleFor(x=>x.ExpirationMonth).GreaterThan(0).LessThan(13).NotEmpty().NotNull().WithMessage("Expiration Month is not correct!");
           RuleFor(x => x.ExpirationYear).GreaterThan(DateTime.Now.Year - 1).GreaterThan(DateTime.Now.Month - 1).NotNull().NotEmpty().WithMessage("Expiration Year is not correct!");
           RuleFor(x => x.FullName).NotEmpty().NotNull().MinimumLength(5).WithMessage("Name is not correct!");
       }
    }
}