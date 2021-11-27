using System.Text.RegularExpressions;
using Core.Entities.Concrete;
using Entities.DataTransferObjects;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserUpdateValidator : AbstractValidator<UserForEdit>
    {
        public UserUpdateValidator()
        {
            Regex onlyLetters = new Regex("^[a-zA-Z]*$");
            Regex onlyNumbers = new Regex("^[0-9]*$");

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Name cannot be empty!");
            RuleFor(u => u.FirstName).Matches(onlyLetters).WithMessage("Name must only have letters!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last Name cannot be empty!");
            RuleFor(u => u.FirstName).Matches(onlyLetters).WithMessage("Last name must only have numbers!");

            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty!");
            RuleFor(u => u.PhoneNumber).Matches(onlyNumbers).WithMessage("Phone number must only have letters!");

            RuleFor(u => u.City).NotEmpty().WithMessage("City cannot be empty!");
            RuleFor(u => u.City).Matches(onlyLetters).WithMessage("Last name must only have numbers!");

            RuleFor(u => u.District).NotEmpty().WithMessage("District cannot be empty!");
            RuleFor(u => u.District).Matches(onlyLetters).WithMessage("Last name must only have numbers!");

            RuleFor(u => u.Address).NotEmpty().WithMessage("Address cannot be empty!");
        }
    }
}