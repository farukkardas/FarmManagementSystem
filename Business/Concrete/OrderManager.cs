using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private  readonly IAuthService _authService;

        public OrderManager(IOrderDal orderDal, IAuthService authService)
        {
            _orderDal = orderDal;
            _authService = authService;
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public IDataResult<List<Order>> GetAll()
        {
            var result = _orderDal.GetAll();

            return new SuccessDataResult<List<Order>>(result);
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public IDataResult<Order> GetById(int id)
        {
            var result = _orderDal.Get(o => o.Id == id);

            return new SuccessDataResult<Order>(result);
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        public IResult Add(Order order, int id, string securityKey)
        {
            _orderDal.Add(order);

            return new SuccessResult($"Order {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        public IResult Delete(Order order, int id, string securityKey)
        {
            _orderDal.Delete(order);

            return new SuccessResult($"Order {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        public IResult Update(Order order, int id, string securityKey)
        {
            _orderDal.Update(order);

            return new SuccessResult($"Order {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("customer,admin")]
        [TransactionScopeAspect]
        public IResult GiveOrder(Order order, int id, string securityKey)
        {
            _orderDal.Add(order);

            return new SuccessResult($"Order {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("customer,user,admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        [TransactionScopeAspect]
        public IResult CancelOrder(int orderId, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<OrderDetailDto>>(conditionResult.Message);
            }
            
            var order = _orderDal.Get(o => o.Id == orderId);

            order.Status = 1;
            
            _orderDal.Update(order);

            return new SuccessResult($"Order has ben cancelled.");
        }

        [SecuredOperations("user,admin,customer")]
        public IDataResult<List<OrderDetailDto>> GetUserOrders(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<OrderDetailDto>>(conditionResult.Message);
            }
            
            var result = _orderDal.GetUserOrders(o=>o.SellerId == id);

            return new SuccessDataResult<List<OrderDetailDto>>(result);
        }
        
        public IDataResult<List<OrderDetailDto>> GetCustomerOrders(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<OrderDetailDto>>(conditionResult.Message);
            }
            
            var result = _orderDal.GetUserOrders(o=>o.CustomerId == id);

            return new SuccessDataResult<List<OrderDetailDto>>(result);
        }

        public IResult ApproveOrder(int id, string securityKey, int orderId)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorResult(conditionResult.Message);
            }

            var result =  _orderDal.Get(o => o.Id == orderId);
            result.Status = 3;
            _orderDal.Update(result);

            return new SuccessResult($" Order has ben approved.");
        }

        public IResult AddCargoNumber(int id, string securityKey, int orderId,int deliveryNo)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorResult(conditionResult.Message);
            }
            
            var result =  _orderDal.Get(o => o.Id == orderId);
            result.DeliveryNo = deliveryNo;
            result.Status = 5;
            _orderDal.Update(result);

            return new SuccessResult("Delivery no has ben approved.");
        }
    }
}