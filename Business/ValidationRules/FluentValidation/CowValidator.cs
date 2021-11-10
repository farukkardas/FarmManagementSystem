using System.Linq;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CowValidator : AbstractValidator<Cow>
    {
        public CowValidator()
        {
            RuleFor(c => c.CowId).NotEmpty().WithMessage("Id cannot be empty!");
            RuleFor(c => c.CowId).Must(IsIdUnique).WithMessage("Cow ID must be unique");
            RuleFor(c => c.Age).GreaterThan(0).WithMessage("Age must greater than zero!");
        }

        private bool IsIdUnique(int arg)
        {
            FarmManagementContext context = new FarmManagementContext();

            var result = context.Cows
                .FirstOrDefault(c => c.CowId == arg);

            if (result == null)
            {
                return true;
            }

            return false;
        }
    }
}