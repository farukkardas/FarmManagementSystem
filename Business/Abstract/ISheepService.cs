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
        IResult Add(Sheep sheep,int id, string securityKey);
        IResult Delete(Sheep sheep,int id, string securityKey);
        IResult Update(Sheep sheep,int id, string securityKey);
        IDataResult<List<Sheep>> GetUserSheeps(int id, string securityKey);
    }
}