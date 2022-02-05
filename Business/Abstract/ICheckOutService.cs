using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICheckOutService
    {
        Task<IResult> CheckoutProducts(int id, string securityKey, CreditCart creditCartInfo);
    }
}