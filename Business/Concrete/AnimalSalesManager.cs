using System.Collections.Generic;
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
    public class AnimalSalesManager : IAnimalSalesService
    {
        private readonly IAnimalSalesDal _animalSalesDal;
        private readonly IAuthService _authService;

        public AnimalSalesManager(IAnimalSalesDal animalSalesDal, IAuthService authService)
        {
            _animalSalesDal = animalSalesDal;
            _authService = authService;
        }

        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public async Task<IDataResult<List<AnimalSales>>> GetAll()
        {
            var result = await _animalSalesDal.GetAll();

            return new SuccessDataResult<List<AnimalSales>>(result);
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<AnimalSales>> GetById(int id)
        {
            var result = await _animalSalesDal.Get(a => a.Id == id);

            return new SuccessDataResult<AnimalSales>(result);
        }


        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IAnimalSaleService.Get")]
        public async Task<IResult> Add(AnimalSales animalSales, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return conditionResult;
            }

            _animalSalesDal.Add(animalSales);

            return new SuccessResult($"Animal sales {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IAnimalSaleService.Get")]
        public async Task<IResult> Delete(AnimalSales animalSales, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return conditionResult;
            }

            _animalSalesDal.Delete(animalSales);

            return new SuccessResult($"Animal sales {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IAnimalSaleService.Get")]
        public async Task<IResult> Update(AnimalSales animalSales, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return conditionResult;
            }

            _animalSalesDal.Update(animalSales);

            return new SuccessResult($"Animal sales {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("user,admin")]
        public async Task<IDataResult<List<AnimalSalesDto>>> GetUserAnimalSales(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<AnimalSalesDto>>(conditionResult.Message);
            }

            var result =  _animalSalesDal.GetAnimalSales(s => s.SellerId == id);

            return new SuccessDataResult<List<AnimalSalesDto>>(result);
        }
    }
}