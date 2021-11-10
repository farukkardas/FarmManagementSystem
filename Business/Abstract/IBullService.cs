using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBullService
    {
        IDataResult<List<Bull>> GetAll();
        IDataResult<Bull> GetById(int id);
        IResult Add(Bull bull);
        IResult Delete(Bull bull);
        IResult Update(Bull bull);

        IDataResult<List<Bull>> GetUserBulls(int id, string securityKey);
    }
}