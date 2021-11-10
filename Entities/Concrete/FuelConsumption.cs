using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entites;

namespace Entities.Concrete
{
   public class FuelConsumption : IEntity
   {
      public int Id { get; set; }
      public string FuelType { get; set; }
      public double Amount { get; set; }
      public double Price { get; set; }
      public DateTime BoughtDate { get; set; }
      public int OwnerId { get; set; }

   }
}
