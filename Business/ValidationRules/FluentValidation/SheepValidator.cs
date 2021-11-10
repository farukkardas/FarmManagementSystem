using System.Linq;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SheepValidator : AbstractValidator<Sheep>
    {
        public SheepValidator()
        {
            RuleFor(s => s.SheepId).NotEmpty().WithMessage("Sheep ID cannot be empty!");
            RuleFor(s => s.SheepId).Must(IsIdUnique).WithMessage("Sheep ID must be unique!");
        }
        
        private bool IsIdUnique(int arg)
        {
            FarmManagementContext context = new FarmManagementContext();

            var result = context.Sheeps
                .FirstOrDefault(s=>s.SheepId == arg);

            if (result == null)
            {
                return true;
            }

            return false;
        }
    }
}