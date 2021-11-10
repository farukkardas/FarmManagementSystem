using System.Data;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using System.Linq;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Business.ValidationRules.FluentValidation
{
    public class BullValidator : AbstractValidator<Bull>
    {
        public BullValidator()
        {
            RuleFor(b => b.BullId).NotEmpty().WithMessage("Id cannot be empty!");
            RuleFor(b => b.BullId).Must(IsIdUnique).WithMessage("Bull ID must be unique!");

        }


        private bool IsIdUnique(int bullId)
        {
            FarmManagementContext context = new FarmManagementContext();

            var result = context.Bulls
                .FirstOrDefault(s => s.BullId == bullId);

            if(result == null)
            {
                return true;
            }

            return false;
        }
    }
}