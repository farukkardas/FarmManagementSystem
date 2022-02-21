using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MailService : IMailService
    {
        private readonly IUserService _userService;

        public MailService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SendMail(EmailObject emailObject)
        {
            var user = await _userService.GetById(emailObject.userId);

            SmtpClient smtpClient = new()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = new NetworkCredential("farukkardasx@gmail.com", "05366510050Ab*-")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("farukkardasx@gmail.com", "Faruk Kardaş")
            };
            
            string filePath = "D:\\Development\\RiderProjects\\FarmManagementSystem\\Business\\MailTemplates\\MailTemplate.html";  
            StreamReader str = new StreamReader(filePath);  
            string MailText = await str.ReadToEndAsync();
            str.Close();  
            MailText = MailText.Replace("[newusername]", emailObject.MailBody);  

            mailMessage.To.Add(user.Data.Email);
            mailMessage.CC.Add(user.Data.Email);
            mailMessage.Subject = emailObject.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = MailText;

            try
            {
                smtpClient.SendAsync(mailMessage,null);
            }
            catch (Exception e)
            {
              //
            }
        }
    }
}