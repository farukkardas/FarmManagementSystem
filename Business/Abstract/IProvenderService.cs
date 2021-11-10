using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProvenderService
    {
        IDataResult<List<Provender>> GetAll();
        IDataResult<Provender> GetById(int id);
        IResult Add(Provender cow);
        IResult Delete(Provender cow);
        IResult Update(Provender cow);
        IDataResult<List<Provender>> GetUserProvenders(int id, string securityKey);
    }
}