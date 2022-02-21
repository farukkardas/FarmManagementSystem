using DataAccess.Entites;

namespace Entities.Concrete
{
    public class EmailObject : IEntity
    {
        public int userId { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
    }
}