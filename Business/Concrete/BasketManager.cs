using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;
        private readonly IAuthService _authService;
        private readonly IProductsOnSaleDal _productsOnSale;
        public BasketManager(IBasketDal basketDal, IAuthService authService, IProductsOnSaleDal productsOnSale)
        {
            _basketDal = basketDal;
            _authService = authService;
            _productsOnSale = productsOnSale;
        }

        public IResult AddToBasket(ProductInBasket productInBasket, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey),CheckIfProductExists(productInBasket.ProductId),CheckIfProductExistOnBasket(productInBasket.ProductId),CheckIfOwnProduct(id,productInBasket.ProductId));
            
            
            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }
            
            

            _basketDal.Add(productInBasket);

            return new SuccessResult("Product successfully added in a basket!");
        }

        private IResult CheckIfOwnProduct(int userId,int productId)
        {
            var productOnSale = _productsOnSale.GetProductById(p => p.Id == productId);

            if (productOnSale.SellerId == userId)
            {
                return new ErrorResult("You cannot add your product in basket!");
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductExistOnBasket(int productId)
        {
            var result = _basketDal.Get(b => b.ProductId == productId);

            if (result != null)
            {
                return new ErrorResult("You have this product in basket!");
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductExists(int productId)
        {
            var product = _productsOnSale.Get(p => p.Id == productId);

            if (product == null)
            {
                return new ErrorResult("Product not found!");
            }

            return new SuccessResult();
        }

        public IResult DeleteFromBasket(ProductInBasket productInBasket, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }
            

            _basketDal.Delete(productInBasket);

            return new SuccessResult();
        }

        public IDataResult<List<BasketProductDto>> GetBasketProducts(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }

            var result = _basketDal.GetBasketProducts(u => u.UserId == id);

            return new SuccessDataResult<List<BasketProductDto>>(result);
        }
    }
}