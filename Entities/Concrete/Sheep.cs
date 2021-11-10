using DataAccess.Entites;

namespace Entities.Concrete
{
    public class Sheep : IEntity
    {
        public int Id { get; set; }
        public int SheepId { get; set; }
        public int Age { get; set; }
        public string Race { get; set; }
        public double Weight { get; set; }
        public int OwnerId { get; set; }
    }
}