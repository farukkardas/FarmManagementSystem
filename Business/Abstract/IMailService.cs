using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMailService
    {
        Task SendMail(EmailObject emailObject);
    }
}