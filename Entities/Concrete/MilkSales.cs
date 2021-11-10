using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Concrete
{
    public class MilkSales : IEntity
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double SalePrice { get; set; }
        public int CustomerId { get; set; }
        public DateTime BoughtDate { get; set; }

        public int SellerId { get; set; }
    }
}