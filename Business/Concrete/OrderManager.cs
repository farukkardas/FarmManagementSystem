using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public OrderManager(IOrderDal orderDal, IAuthService authService, IMailService mailService,
            IUserService userService)
        {
            _orderDal = orderDal;
            _authService = authService;
            _mailService = mailService;
            _userService = userService;
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<List<Order>>> GetAll()
        {
            var result = await _orderDal.GetAll();

            return new SuccessDataResult<List<Order>>(result);
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<Order>> GetById(int id)
        {
            var result = await _orderDal.Get(o => o.Id == id);

            return new SuccessDataResult<Order>(result);
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        public async Task<IResult> Add(Order order, int id, string securityKey)
        {
            await _orderDal.Add(order);

            return new SuccessResult($"Order {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        public async Task<IResult> Delete(Order order, int id, string securityKey)
        {
            await _orderDal.Delete(order);

            return new SuccessResult($"Order {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        public async Task<IResult> Update(Order order, int id, string securityKey)
        {
            await _orderDal.Update(order);

            return new SuccessResult($"Order {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("customer,admin")]
        [TransactionScopeAspect]
        public async Task<IResult> GiveOrder(Order order, int id, string securityKey)
        {
            await _orderDal.Add(order);

            return new SuccessResult($"Order {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("customer,user,admin")]
        [CacheRemoveAspect("IOrderDal.Get")]
        [TransactionScopeAspect]
        public async Task<IResult> CancelOrder(int orderId, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<OrderDetailDto>>(conditionResult.Message);
            }

            var order = await _orderDal.Get(o => o.Id == orderId);
            order.Status = 1;
            await _orderDal.Update(order);
            
            
            var customer = await _userService.GetById(order.CustomerId);
            EmailObject emailObject = new EmailObject
            {
                userId = customer.Data.Id,
                Subject = "Your product is canceled!",
                MailBody =
                    $"Dear {customer.Data.FirstName} {customer.Data.LastName},  {order.Id} numbered order is canceled by seller."
            };
            await _mailService.SendMail(emailObject);

            
            return new SuccessResult($"Order has ben cancelled.");
        }

        [SecuredOperations("user,admin,customer")]
        public async Task<IDataResult<List<OrderDetailDto>>> GetUserOrders(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<OrderDetailDto>>(conditionResult.Message);
            }

            var result = await _orderDal.GetUserOrders(o => o.SellerId == id);

            return new SuccessDataResult<List<OrderDetailDto>>(result);
        }

        public async Task<IDataResult<List<OrderDetailDto>>> GetCustomerOrders(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<OrderDetailDto>>(conditionResult.Message);
            }

            var result = await _orderDal.GetUserOrders(o => o.CustomerId == id);

            return new SuccessDataResult<List<OrderDetailDto>>(result);
        }

        public async Task<IResult> ApproveOrder(int id, string securityKey, int orderId)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorResult(conditionResult.Message);
            }

            var result = await _orderDal.Get(o => o.Id == orderId);
            result.Status = 3;
            await _orderDal.Update(result);
           
            var customer = await _userService.GetById(result.CustomerId);
          
            EmailObject emailObject = new EmailObject
            {
                userId = customer.Data.Id,
                Subject = "Your product is successfully approved!",
                MailBody =
                    $"Dear {customer.Data.FirstName} {customer.Data.LastName},  {result.Id} numbered order is approved. You will be informed when your product is delivered to the cargo."
            };
            await _mailService.SendMail(emailObject);

            return new SuccessResult($" Order has ben approved.");
        }

        public async Task<IResult> AddCargoNumber(int id, string securityKey, int orderId, int deliveryNo)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorResult(conditionResult.Message);
            }

            var result = await _orderDal.Get(o => o.Id == orderId);
            result.DeliveryNo = deliveryNo;
            result.Status = 5;
            await _orderDal.Update(result);
            
            var customer = await _userService.GetById(result.CustomerId);
            EmailObject emailObject = new EmailObject
            {
                userId = customer.Data.Id,
                Subject = "Your product is delivered the cargo!",
                MailBody =
                    $"Dear {customer.Data.FirstName} {customer.Data.LastName},  {result.Id} numbered order is delivered cargo. Your cargo number is {result.DeliveryNo}. You can track order with the cargo number."
            };
            await _mailService.SendMail(emailObject);

            return new SuccessResult("Delivery no has ben approved.");
        }
    }
}