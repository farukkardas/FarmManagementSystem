using DataAccess.Entites;

namespace Entities.Concrete
{
    public class Bull : IEntity
    {
        public int Id { get; set; }
        public int BullId { get; set; }
        public int Age { get; set; }
        public string BullName { get; set; }
        public double Weight { get; set; }
        public int OwnerId { get; set; }
    }
}