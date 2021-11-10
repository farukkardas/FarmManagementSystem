using System.Linq;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CalfValidator:AbstractValidator<Calves>
    {
        public CalfValidator()
        {
            RuleFor(c => c.CalfId).NotEmpty().WithMessage("Id cannot be empty!");
            RuleFor(c => c.CalfId).Must(IsIdUnique).WithMessage("Calf ID must be unique!");

        }

        private bool IsIdUnique(int arg)
        {
            FarmManagementContext context = new FarmManagementContext();

            var result = context.Calves
                .FirstOrDefault(c => c.CalfId == arg);

            if (result == null)
            {
                return true;
            }

            return false;
        }
    }
}