using Domaine.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TourMe.Data;

namespace Repository.Implementation
{
    public class EmailRepo : IEmailRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<SendEmail> GenericSendEmail;

        public EmailRepo(TourMeContext dbContext, IGenericRepository<SendEmail> _GenericRepoFournisseur)
        {
            this.dbContext = dbContext;
            GenericSendEmail = _GenericRepoFournisseur;
        }

        public async Task<SendEmail> Details(string id)
        {
            var SendEmail = await dbContext.SendEmail.SingleOrDefaultAsync(w => w.Id.Equals(id));
            dbContext.Entry(SendEmail).Reference(x => x.Mailcommercant).Query().Load();
            dbContext.Entry(SendEmail).State = EntityState.Detached;
            return SendEmail;
        }

        public IQueryable<SendEmail> GetAllMail()
        {
            var list = dbContext.SendEmail.Where(send => send.Id != null).Include(x=>x.Mailcommercant).Include(w=>w.File);
            return list;
        }

        public  string SendEmail(string name, string pass, MailMessage msg, SendEmail email)
        {
            
            
           using ( SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("wissem.khaskhoussy@esprit.tn", "wiss20/20");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    GenericSendEmail.InsertAsync(email);
                    client.Send(msg);
                    
                    return " send email successful";
                }
                catch (Exception e)
                {
                    
                    return " send email failde";
                }

            }
        }


    }
}
