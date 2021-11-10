using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class FertilizerManager : IFertilizerService
    {
        private readonly IFertilizerDal _fertilizerDal;
        private readonly IUserDal _userDal;

        public FertilizerManager(IFertilizerDal fertilizerDal, IUserDal userDal)
        {
            _fertilizerDal = fertilizerDal;
            _userDal = userDal;
        }

        public IDataResult<List<Fertilizer>> GetAll()
        {
            var result = _fertilizerDal.GetAll();

            return new SuccessDataResult<List<Fertilizer>>(result);
        }

        public IDataResult<Fertilizer> GetById(int id)
        {
            var result = _fertilizerDal.Get(f => f.Id == id);

            return new SuccessDataResult<Fertilizer>(result);
        }

        public IResult Add(Fertilizer fertilizer)
        {
            _fertilizerDal.Add(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyAdded}");
        }

        public IResult Delete(Fertilizer fertilizer)
        {
            _fertilizerDal.Delete(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyDeleted}");
        }

        public IResult Update(Fertilizer fertilizer)
        {
            _fertilizerDal.Add(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<Fertilizer>> GetUserFertilizers(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<Fertilizer>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<Fertilizer>>("You have not permission for this.");
            }

            var fertilizers = _fertilizerDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Fertilizer>>(fertilizers);
        }
    }
}