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

        public BasketManager(IBasketDal basketDal, IAuthService authService)
        {
            _basketDal = basketDal;
            _authService = authService;
        }

        public IResult AddToBasket(ProductInBasket productInBasket, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<BasketProductDto>>(conditionResult.Message);
            }

            _basketDal.Add(productInBasket);

            return new SuccessResult($"Product {Messages.SuccessfullyAdded} in a basket!");
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