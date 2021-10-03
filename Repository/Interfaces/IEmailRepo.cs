using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface IEmailRepo
    {
        public string SendEmail(string name, string pass, MailMessage msg, SendEmail email);
        public IQueryable<SendEmail> GetAllMail();
        public Task<SendEmail> Details(string id);
    }
}
