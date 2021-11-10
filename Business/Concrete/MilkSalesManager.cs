using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Concrete
{
    public class MilkSalesManager : IMilkSalesService
    {
        private readonly IMilkSalesDal _milkSalesDal;
        private readonly IUserDal _userDal;

        public MilkSalesManager(IMilkSalesDal milkSalesDal, IUserDal userDal)
        {
            _milkSalesDal = milkSalesDal;
            _userDal = userDal;
        }

        public IDataResult<List<MilkSales>> GetAll()
        {
            var result = _milkSalesDal.GetAll();

            return new SuccessDataResult<List<MilkSales>>(result);
        }

        public IDataResult<MilkSales> GetById(int id)
        {
            var result = _milkSalesDal.Get(m => m.Id == id);

            return new SuccessDataResult<MilkSales>(result);
        }

        public IDataResult<List<MilkSalesDto>> GetMilkSales()
        {
            var result = _milkSalesDal.GetMilkSales();

            return new SuccessDataResult<List<MilkSalesDto>>(result);
        }


        public IResult Add(MilkSales milkSales)
        {
            _milkSalesDal.Add(milkSales);

            return new SuccessResult($"Milk sale {Messages.SuccessfullyAdded}");
        }

        public IResult Delete(MilkSales milkSales)
        {
            _milkSalesDal.Delete(milkSales);

            return new SuccessResult($"Milk sale {Messages.SuccessfullyDeleted}");
        }

        public IResult Update(MilkSales milkSales)
        {
            _milkSalesDal.Update(milkSales);

            return new SuccessResult($"Milk sale {Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<MilkSalesDto>> GetUserMilkSales(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<MilkSalesDto>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<MilkSalesDto>>("You have not permission for this.");
            }

            var milkSales = _milkSalesDal.GetMilkSales(c=>c.SellerId == id);
            
            return new SuccessDataResult<List<MilkSalesDto>>(milkSales);
        }
    }
}