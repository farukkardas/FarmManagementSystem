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
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz!");
            RuleFor(p => p.FirstName).MinimumLength(2).WithMessage("İsim en az 2 harfli olmalıdır!");
            RuleFor(p => p.FirstName).MaximumLength(39).WithMessage("İsim maksimum 40 karakter olabilir!");
            
            
            //Last Name
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Soyisim boş bırakılamaz!");
            RuleFor(p => p.LastName).MinimumLength(2).WithMessage("Soyisim en az 2 harfli olmalıdır!");
            RuleFor(p => p.LastName).MaximumLength(39).WithMessage("İsim maksimum 40 karakter olabilir!");
           
            
            //Email
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email boş bırakılamaz!");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçersiz mail formatı!");
            RuleFor(p => p.Email).MaximumLength(299).WithMessage("Email en fazla 300 karakter olabilir!");
            RuleFor(p => p.Email).MinimumLength(10).WithMessage("Email minimum 10 karakter olabilir!");
            RuleFor(p => p.Email).Must(IsEmailUnique).WithMessage("Bu emaile kayıtlı kullanıcı bulunuyor!");
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