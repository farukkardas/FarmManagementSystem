using System.Collections.Generic;
using System.Drawing;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICalfService
    {
        IDataResult<List<Calves>> GetAll();
        IDataResult<Calves> GetById(int id);
        IResult Add(Calves calves,int id ,string securityKey);
        IResult Delete(Calves calves,int id ,string securityKey);
        IResult Update(Calves calves,int id ,string securityKey);
        IDataResult<List<Calves>> GetUserCalves(int id, string securityKey);
    }
}