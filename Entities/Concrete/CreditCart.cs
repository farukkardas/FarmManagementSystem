using DataAccess.Entites;

namespace Entities.Concrete
{
    public class CreditCart : IEntity
    {
        public string FullName { get; set; }
        public string CartNumber { get; set; }
        public string CvvNumber { get; set; }
        public string ExpirationDate { get; set; }
      
    }
}