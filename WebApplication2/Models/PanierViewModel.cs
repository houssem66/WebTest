using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace TourMe.Web.Models
{
    public class PanierViewModel
    {
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public DateTime DatedebutL { get; set; }
        public DateTime DateFinL { get; set; }
        public int NbrPlat { get; set; }
        public DateTime DateDispoTrans { get; set; }
        public string RemarquesLogement { get; set; }
        public string RemarquesTransport { get; set; }
        public string RemarquesNourriture { get; set; }
        public IList<Experience> experiences  { get; set; }
        public IList<ServiceLogment> Logments { get; set; }
        public IList<ServiceNouritture> nourittures { get; set; }
        public IList<ServiceTransport> Transports { get; set; }


    }
}
