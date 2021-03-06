using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Internal;
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
        private IBasketService _basketService;
        private readonly IAuthService _authService;
        private readonly IProductsOnSaleDal _productsOnSaleDal;
        private readonly IOrderDal _orderDal;
        private readonly IUserDal _userDal;

        public CheckoutManager(IBasketDal basketDal, IAuthService authService, IProductsOnSaleDal productsOnSaleDal,
            IOrderDal orderDal, IUserDal userDal, IBasketService basketService)
        {
            _basketDal = basketDal;
            _authService = authService;
            _productsOnSaleDal = productsOnSaleDal;
            _orderDal = orderDal;
            _userDal = userDal;
            _basketService = basketService;
        }

        [SecuredOperations("user,admin,customer")]
        [ValidationAspect(typeof(CcValidator))]
        [TransactionScopeAspect]
        public async Task<IResult> CheckoutProducts(int id, string securityKey, CreditCart creditCartInfo)
        {
            var baskedProducts = await _basketDal.GetAll(b => b.UserId == id);
            var userDetails = await _userDal.Get(u => u.Id == id);

            IResult conditionRules = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey),
                await Task.Run(() => CheckBasketIsEmpty(baskedProducts)),
                await Task.Run(() => DontBuyOwnProduct(id, baskedProducts)),
                await CheckIfAddressExists(id));

            if (conditionRules != null)
            {
                return new ErrorResult(conditionRules.Message);
            }

// Do communication with BANK'S API'S and make payment here.            


            foreach (var basketP in baskedProducts)
            {
                var boughtProduct = await _productsOnSaleDal.GetProductById(p => p.Id == basketP.ProductId);
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
                    Status = 2
                };
               await _orderDal.Add(order);
            }

            foreach (var deleteProduct in baskedProducts)
            {
                await _basketService.Delete(deleteProduct);
            }

            return new SuccessResult("Your order has been received successfully.");
        }

        private async Task<IResult> CheckIfAddressExists(int id)
        {
            var result = await _userDal.GetUserDetails(u => u.Id == id);

            if (result.Address.IsNullOrEmpty() || result.City == null)
            {
                return new ErrorResult("To complete checkout please fill your address in your profile page!");
            }

            return new SuccessResult();
        }

        private async Task<IResult> DontBuyOwnProduct(int id, List<ProductInBasket> baskedProducts)
        {
            foreach (var baskedProduct in baskedProducts)
            {
                var productsOnSales = await _productsOnSaleDal.GetAll(p => p.Id == baskedProduct.ProductId);

                foreach (var p in productsOnSales)
                {
                    if (p.SellerId == id)
                    {
                        return new ErrorResult("You dont buy your product!");
                    }
                }
            }

            return new SuccessResult();
        }


        private IResult CheckBasketIsEmpty(List<ProductInBasket> baskedProducts)
        {
            if (baskedProducts.Count < 1)
            {
                return new ErrorResult("You dont have product in basket!");
            }

            return new SuccessResult();
        }
    }
}