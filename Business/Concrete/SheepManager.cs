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
    public class SheepManager : ISheepService
    {
        private readonly ISheepDal _sheepDal;
        private readonly IUserDal _userDal;

        public SheepManager(ISheepDal sheepDal, IUserDal userDal)
        {
            _sheepDal = sheepDal;
            _userDal = userDal;
        }

        [CacheAspect(10)]
        public IDataResult<List<Sheep>> GetAll()
        {
            var result = _sheepDal.GetAll();

            return new SuccessDataResult<List<Sheep>>(result);
        }

        [CacheAspect(10)]
        public IDataResult<Sheep> GetById(int id)
        {
            var result = _sheepDal.Get(s => s.Id == id);

            return new SuccessDataResult<Sheep>(result);
        }

        [ValidationAspect(typeof(SheepValidator))]
        [CacheRemoveAspect("ISheepService.Get")]
        public IResult Add(Sheep sheep)
        {
            _sheepDal.Add(sheep);

            return new SuccessResult($"Sheep{Messages.SuccessfullyAdded}");
        }

        [CacheRemoveAspect("ISheepService.Get")]
        public IResult Delete(Sheep sheep)
        {
            _sheepDal.Delete(sheep);

            return new SuccessResult($"Sheep{Messages.SuccessfullyDeleted}");
        }

        [CacheRemoveAspect("ISheepService.Get")]
        public IResult Update(Sheep sheep)
        {
            _sheepDal.Update(sheep);

            return new SuccessResult($"Sheep{Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<Sheep>> GetUserSheeps(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<Sheep>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<Sheep>>("You have not permission for this.");
            }

            var sheeps = _sheepDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Sheep>>(sheeps);
            
        }
    }
}