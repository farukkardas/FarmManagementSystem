using System.Collections.Generic;
using System.Drawing;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICowService
    {
        IDataResult<List<Cow>> GetAll();
        IDataResult<Cow> GetById(int id);
        
        IDataResult<Cow> GetByCowId(int cowId);
        IResult Add(Cow cow);
        IResult Delete(Cow cow);
        IResult Update(Cow cow);

        IDataResult<List<Cow>> GetUserCows(int id, string securityKey);

       
    }
}