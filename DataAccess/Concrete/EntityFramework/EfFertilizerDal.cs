using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFertilizerDal : EfEntityRepositoryBase<Fertilizer,FarmManagementContext>, IFertilizerDal
    {
        
    }
}