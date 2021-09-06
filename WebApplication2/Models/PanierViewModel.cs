using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace TourMe.Web.Models
{
    public class PanierViewModel
    {
        public decimal? Prix { get; set; }
        public int? Quantite { get; set; }
        public DateTime? DatedebutL { get; set; }
        public DateTime? DateDispoTrans { get; set; }
        public DateTime? DateReservationRes { get; set; }
        public int? NbrJoursLogement { get; set; }
        public int? NbrPlat { get; set; }

        public int? NbrJoursTrans { get; set; }
        public string RemarquesLogement { get; set; }
        public string RemarquesTransport { get; set; }
        public string RemarquesNourriture { get; set; }
        public  IList<Experience> Experiences { get; set; }
        public  IList<ServiceLogment> Logments { get; set; }
        public  IList<ServiceNouritture> Nourittures { get; set; }
        public virtual IList<ServiceTransport> Transports { get; set; }



    }
}
