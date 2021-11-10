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
    public class CalfManager : ICalfService
    {
        private readonly ICalfDal _calfDal;
        private readonly IUserDal _userDal;
        
        public CalfManager(ICalfDal calfDal, IUserDal userDal)
        {
            _calfDal = calfDal;
            _userDal = userDal;
        }

        public IDataResult<List<Calves>> GetAll()
        {
            var result = _calfDal.GetAll();
            return new SuccessDataResult<List<Calves>>(result);
        }

        public IDataResult<Calves> GetById(int id)
        {
            var result = _calfDal.Get(c => c.Id == id);
            return new SuccessDataResult<Calves>(result);
        }

        [ValidationAspect(typeof(CalfValidator))]
        [CacheRemoveAspect("ICalfService.Get")]
        public IResult Add(Calves calves)
        {
            _calfDal.Add(calves);
            return new SuccessResult($"Calf{Messages.SuccessfullyAdded}");
        }

        [CacheRemoveAspect("ICalfService.Get")]
        public IResult Delete(Calves calves)
        {
            _calfDal.Delete(calves);
            return new SuccessResult($"Calf{Messages.SuccessfullyDeleted}");
        }

        [CacheRemoveAspect("ICalfService.Get")]
        public IResult Update(Calves calves)
        {
            _calfDal.Update(calves);
            return new SuccessResult($"Calf{Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<Calves>> GetUserCalves(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<Calves>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<Calves>>("You have not permission for this.");
            }

            var calves = _calfDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Calves>>(calves);
        }
    }
}