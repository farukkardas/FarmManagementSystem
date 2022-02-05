using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IOrderService
    {
        //admin methods
        Task<IDataResult<List<Order>>> GetAll();
        Task<IDataResult<Order>> GetById(int id);
        Task<IResult> Add(Order order,int id,string securityKey);
        Task<IResult> Delete(Order order,int id,string securityKey);
        Task<IResult> Update(Order order,int id,string securityKey);
        //Customer methods
        Task<IResult> GiveOrder(Order order,int id,string securityKey);
        Task<IResult> CancelOrder(int orderId,int id,string securityKey);
        //Seller Methods
        Task<IDataResult<List<OrderDetailDto>>> GetUserOrders(int id,string securityKey);
        Task<IDataResult<List<OrderDetailDto>>> GetCustomerOrders(int id,string securityKey);
        Task<IResult> ApproveOrder(int id, string securityKey, int orderId);
        Task<IResult> AddCargoNumber(int id, string securityKey, int orderId,int deliveryNo);
    }
}