using System;
using System.Threading.Tasks;
using DataAccess.Entites;

namespace Entities.Concrete
{
    public class UserImage : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
        public DateTime? ImageDate { get; set; }
    }
}