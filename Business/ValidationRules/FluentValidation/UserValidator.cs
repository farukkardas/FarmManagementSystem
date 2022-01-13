using System.Linq;
using Core.Entities.Concrete;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            //First name
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Name can not be empty!");
            RuleFor(p => p.FirstName).MinimumLength(2).WithMessage("Name minimum 2 character!");
            RuleFor(p => p.FirstName).MaximumLength(39).WithMessage("Name maximum 40 character!");
            
            
            //Last Name
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Lastname cannot be empty!");
            RuleFor(p => p.LastName).MinimumLength(2).WithMessage("Lastname minimum 2 length!");
            RuleFor(p => p.LastName).MaximumLength(39).WithMessage("Lastname maximum 40 length!");
           
            
            //Email
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email can not be empty!");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Invalid mail format!");
            RuleFor(p => p.Email).MaximumLength(299).WithMessage("Email maximum 300 character!");
            RuleFor(p => p.Email).MinimumLength(10).WithMessage("Email minimum length is 10!");
            RuleFor(p => p.Email).Must(IsEmailUnique).WithMessage("There is a registered user for this e-mail!");
            
            //Address
            RuleFor(p => p.Address).MinimumLength(20).WithMessage("Address minimum length 20 letter!");
            RuleFor(p => p.Address).MaximumLength(299).WithMessage("Address maximum length 300 letter!");
            
            
        }

        private bool IsEmailUnique(string arg)
        {
            FarmManagementContext context = new FarmManagementContext();

            var result = context.Users
                .FirstOrDefault(p => p.Email.ToLower() == arg.ToLower());

            if (result == null)
            {
                return true;
            }

            return false;
        }
    }
}