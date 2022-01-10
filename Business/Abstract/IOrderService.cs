using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IOrderService
    {
        //admin methods
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int id);
        IResult Add(Order order,int id,string securityKey);
        IResult Delete(Order order,int id,string securityKey);
        IResult Update(Order order,int id,string securityKey);
        //Customer methods
        IResult GiveOrder(Order order,int id,string securityKey);
        IResult CancelOrder(int orderId,int id,string securityKey);
        //Seller Methods
        IDataResult<List<OrderDetailDto>> GetUserOrders(int id,string securityKey);
        IDataResult<List<OrderDetailDto>> GetCustomerOrders(int id,string securityKey);
        IResult ApproveOrder(int id, string securityKey, int orderId);
        IResult AddCargoNumber(int id, string securityKey, int orderId,int deliveryNo);
    }
}