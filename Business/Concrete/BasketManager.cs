using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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


        [CacheRemoveAspect("IBasketService.Get")]
        [SecuredOperations("admin,user,customer")]
        public async Task<IResult> AddToBasket(ProductInBasket productInBasket, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey),
                await CheckIfProductExists(productInBasket.ProductId),
                await CheckIfProductExistOnBasket(id, productInBasket.ProductId),
                await CheckIfOwnProduct(id, productInBasket.ProductId));


            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }


            await _basketDal.Add(productInBasket);

            return new SuccessResult("Product successfully added in a basket!");
        }

        private async Task<IResult> CheckIfOwnProduct(int userId, int productId)
        {
            var productOnSale = await _productsOnSale.GetProductById(p => p.Id == productId);

            if (productOnSale.SellerId == userId)
            {
                return new ErrorResult("You cannot add your product in basket!");
            }

            return new SuccessResult();
        }

        private async Task<IResult> CheckIfProductExistOnBasket(int userId, int productId)
        {
            var result = await _basketDal.GetBasketProducts(b => b.UserId == userId);

            if (result.Any(x => x.ProductId == productId))
            {
                return new ErrorResult("You have this product in basket!");
            }

            return new SuccessResult();
        }

        private async Task<IResult> CheckIfProductExists(int productId)
        {
            var product = await _productsOnSale.Get(p => p.Id == productId);

            if (product == null)
            {
                return new ErrorResult("Product not found!");
            }

            return new SuccessResult();
        }

        [CacheRemoveAspect("IBasketService.Get")]
        [SecuredOperations("admin,user,customer")]
        public async Task<IResult> DeleteFromBasket(ProductInBasket productInBasket, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }


            await _basketDal.Delete(productInBasket);

            return new SuccessResult();
        }

        [CacheRemoveAspect("IBasketService.Get")]
        [SecuredOperations("admin,user,customer")]
        public async Task<IResult> Delete(ProductInBasket productInBasket)
        {
            await _basketDal.Delete(productInBasket);

            return new SuccessResult();
        }

        [SecuredOperations("admin,user,customer")]
        [CacheAspect(10)]
        public async Task<IDataResult<List<BasketProductDto>>> GetBasketProducts(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }

            var result = await _basketDal.GetBasketProducts(u => u.UserId == id);

            return new SuccessDataResult<List<BasketProductDto>>(result);
        }
    }
}