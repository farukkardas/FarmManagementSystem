using System;
using DataAccess.Entites;

namespace Entities.Concrete
{
    public class Provender:IEntity
    {
        public int Id { get; set; }
        public string ProvenderName { get; set; }
        public double Weight { get; set; }   
        public double Price { get; set; }
        public DateTime BoughtDate { get; set; }
        public int OwnerId { get; set; }

    }
}