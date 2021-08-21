using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public class Panier
    {    [Key]
        public int Id { get; set; }
        public decimal? Prix { get; set; }
        public int? Quantite { get; set; }
        public DateTime? DatedebutL { get; set; }
        public DateTime? DateFinL { get; set; }
        public int? NbrPlat { get; set; }
        public DateTime? DateDispoTrans { get; set; }
        public string RemarquesLogement { get; set; }
        public string RemarquesTransport { get; set; }
        public string RemarquesNourriture { get; set; }

        //navigation properties
        public Utilisateur User { get; set; }
        public virtual IList<Experience> Experiences { get; set; }  
        public virtual IList<ServiceLogment> Logments { get; set; }
        public virtual IList<ServiceNouritture> Nourittures { get; set; }     
        public virtual IList<ServiceTransport> Transports { get; set; }





    }
}
