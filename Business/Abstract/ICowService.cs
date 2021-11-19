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
        IResult Add(Cow cow,int id , string securityKey);
        IResult Delete(Cow cow,int id , string securityKey);
        IResult Update(Cow cow,int id , string securityKey);

        IDataResult<List<Cow>> GetUserCows(int id, string securityKey);

       
    }
}