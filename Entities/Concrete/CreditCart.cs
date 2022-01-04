using DataAccess.Entites;

namespace Entities.Concrete
{
    public class CreditCart : IEntity
    {
        public string FullName { get; set; }
        public string CartNumber { get; set; }
        public int CvvNumber { get; set; }
        public int ExpirationYear { get; set; }
        public int ExpirationMonth { get; set; }
    }
}