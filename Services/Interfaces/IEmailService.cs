using Domaine.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Services.Interfaces
{
   public interface IEmailService
    {
        public string SendEmail( string name, string pass, MailMessage msg ,SendEmail email);
        public IList<SendEmail> GetMails();
        public Task Delete(string id);
        public Task<SendEmail> GetDetails();
    }
}
