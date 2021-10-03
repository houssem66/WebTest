
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TourMe.Data.Entities;

namespace Domaine.Entities
{
   public class SendEmail
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Commerçant Mailcommercant { get; set; }
        public IList<LNDocuments> File { get; set; }
    }
}
