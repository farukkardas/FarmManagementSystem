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
        IResult Add(Calves calves);
        IResult Delete(Calves calves);
        IResult Update(Calves calves);
        IDataResult<List<Calves>> GetUserCalves(int id, string securityKey);
    }
}