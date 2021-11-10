using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BullManager : IBullService
    {
        private readonly IBullDal _bullDal;
        private readonly IUserDal _userDal;

        public BullManager(IBullDal bullDal, IUserDal userDal)
        {
            _bullDal = bullDal;
            _userDal = userDal;
        }
     
        public IDataResult<List<Bull>> GetAll()
        {
            var result = _bullDal.GetAll();
            return new SuccessDataResult<List<Bull>>(result);
        }
        
        public IDataResult<Bull> GetById(int id)
        {
            var result = _bullDal.Get(b => b.Id == id);
            return new SuccessDataResult<Bull>(result);
        }

        [ValidationAspect(typeof(BullValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(Bull bull)
        {
            _bullDal.Add(bull);
            return new SuccessResult($"Bull{Messages.SuccessfullyAdded}");
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(Bull bull)
        {
            _bullDal.Delete(bull);
            return new SuccessResult($"Bull{Messages.SuccessfullyDeleted}");
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(Bull bull)
        {
            _bullDal.Update(bull);
            return new SuccessResult($"Bull{Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<Bull>> GetUserBulls(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<Bull>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<Bull>>("You have not permission for this.");
            }

            var bulls = _bullDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Bull>>(bulls);
            
        }
    }
}