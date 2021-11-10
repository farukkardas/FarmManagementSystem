using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProvenderManager : IProvenderService
    {
        private readonly IProvenderDal _provenderDal;
        private readonly IUserDal _userDal;

        public ProvenderManager(IProvenderDal provenderDal, IUserDal userDal)
        {
            _provenderDal = provenderDal;
            _userDal = userDal;
        }

        public IDataResult<List<Provender>> GetAll()
        {
            var result = _provenderDal.GetAll();

            return new SuccessDataResult<List<Provender>>(result);
        }

        public IDataResult<Provender> GetById(int id)
        {
            var result = _provenderDal.Get(s => s.Id == id);

            return new SuccessDataResult<Provender>(result);
        }

        public IResult Add(Provender provender)
        {
            _provenderDal.Add(provender);

            return new SuccessResult($"Provender {Messages.SuccessfullyAdded}");
        }

        public IResult Delete(Provender provender)
        {
            _provenderDal.Delete(provender);
            return new SuccessResult($"Provender {Messages.SuccessfullyDeleted}");

        }

        public IResult Update(Provender provender)
        {
            _provenderDal.Update(provender);
            return new SuccessResult($"Provender {Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<Provender>> GetUserProvenders(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<Provender>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<Provender>>("You have not permission for this.");
            }

            var provenders = _provenderDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Provender>>(provenders);
        }
    }
}