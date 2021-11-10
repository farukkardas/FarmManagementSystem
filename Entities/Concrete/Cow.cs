using DataAccess.Entites;

namespace Entities.Concrete
{
    public class Cow : IEntity
    { public int Id { get; set; }
        public int CowId { get; set; }
        public int Age { get; set; }
        public string CowName { get; set; }
        public double Weight { get; set; }
        public double DailyMilkAmount { get; set; }
        public double WeeklyMilkAmount { get; set; }
        public double MonthlyMilkAmount { get; set; }
        public int OwnerId { get; set; }

    }
}