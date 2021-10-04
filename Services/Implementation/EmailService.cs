using Domaine.Entities;

using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class EmailService :IEmailService
    {

        private readonly IGenericRepository<SendEmail> genericRepo;
        private readonly IEmailRepo emailRepo;

        public EmailService(IGenericRepository<SendEmail> genericRepo, IEmailRepo  EmailRepo)
        {
            this.genericRepo = genericRepo;
            emailRepo = EmailRepo;
        }

        public Task Delete(string id)
        {
            return genericRepo.DeleteAsync(id);
        }

        public Task<SendEmail> GetDetails()
        {
            throw new System.NotImplementedException();
        }

        public IList<SendEmail> GetMails()
        {
            var list = emailRepo.GetAllMail();
            return list.ToList();
        }

        public string SendEmail( string name,string pass, MailMessage msg, SendEmail email)
        {
            
            return emailRepo.SendEmail(name, pass, msg,email);
            
        }
     
    }
}
