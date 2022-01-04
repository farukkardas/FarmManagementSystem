using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICheckOutService
    {
        IResult CheckoutProducts(int id, string securityKey, CreditCart creditCartInfo);
    }
}