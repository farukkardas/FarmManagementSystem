using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entites;

namespace Entities.Concrete
{
   public class Fertilizer : IEntity
    {
        public int Id { get; set; }
        public string FertilizerType { get; set; }
        public string FertilizerBrand { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public DateTime BoughtDate { get; set; }
        public int OwnerId { get; set; }

    }
}
