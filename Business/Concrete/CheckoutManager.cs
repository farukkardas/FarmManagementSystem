using System;
using Business.Abstract;
using Business.BusinessAspects;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CheckoutManager : ICheckOutService
    {
        private readonly IBasketDal _basketDal;
        private readonly IAuthService _authService;
        private readonly IProductsOnSaleDal _productsOnSaleDal;
        private readonly IOrderDal _orderDal;
        private readonly IUserDal _userDal;

        public CheckoutManager(IBasketDal basketDal, IAuthService authService, IProductsOnSaleDal productsOnSaleDal,
            IOrderDal orderDal, IUserDal userDal)
        {
            _basketDal = basketDal;
            _authService = authService;
            _productsOnSaleDal = productsOnSaleDal;
            _orderDal = orderDal;
            _userDal = userDal;
        }

        [SecuredOperations("user,admin,customer")]
        [ValidationAspect(typeof(CcValidator))]
        [TransactionScopeAspect]
        public IResult CheckoutProducts(int id, string securityKey, CreditCart creditCartInfo)
        {
            IResult conditionRules = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionRules != null)
            {
                return new ErrorResult(conditionRules.Message);
            }

// Do communication with BANK'S API'S and make payment here.            

            var baskedProducts = _basketDal.GetAll(b => b.UserId == id);
            var userDetails = _userDal.Get(u => u.Id == id);

            foreach (var basketP in baskedProducts)
            {
                var boughtProduct = _productsOnSaleDal.GetProductById(p => p.Id == basketP.ProductId);

                Order order = new Order
                {
                    
                    SellerId = boughtProduct.SellerId,
                    CustomerId = id,
                    ProductId = boughtProduct.Id,
                    ProductType = boughtProduct.CategoryId,
                    Price = boughtProduct.Price,
                    DeliveryCity = userDetails.Id,
                    DeliveryDistrict = userDetails.District ?? " ",
                    DeliveryAddress = userDetails.Address ?? " ",
                    BoughtDate = DateTime.Now,
                    Status = true
                };
                _orderDal.Add(order);
            }

            return new SuccessResult("Your order has been received successfully.");
        }
    }
}