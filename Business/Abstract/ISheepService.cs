using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISheepService
    {
        IDataResult<List<Sheep>> GetAll();
        IDataResult<Sheep> GetById(int id);
        IResult Add(Sheep sheep);
        IResult Delete(Sheep sheep);
        IResult Update(Sheep sheep);
        IDataResult<List<Sheep>> GetUserSheeps(int id, string securityKey);
    }
}