using System;
using DataAccess.Entites;

namespace Entities.Concrete
{
    public class Calves : IEntity
    {
        public int Id { get; set; }
        public int CalfId { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string CalfName { get; set; }
        public double Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public int OwnerId { get; set; }

    }
}