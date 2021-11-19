using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProvenderService
    {
        IDataResult<List<Provender>> GetAll();
        IDataResult<Provender> GetById(int id);
        IResult Add(Provender cow,int id,string securityKey);
        IResult Delete(Provender cow,int id,string securityKey);
        IResult Update(Provender cow,int id,string securityKey);
        IDataResult<List<Provender>> GetUserProvenders(int id, string securityKey);
    }
}