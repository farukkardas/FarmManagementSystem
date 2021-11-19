using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBullService
    {
        IDataResult<List<Bull>> GetAll();
        IDataResult<Bull> GetById(int id);
        IResult Add(Bull bull,int id,string securityKey);
        IResult Delete(Bull bull,int id,string securityKey);
        IResult Update(Bull bull,int id,string securityKey);

        IDataResult<List<Bull>> GetUserBulls(int id, string securityKey);
    }
}