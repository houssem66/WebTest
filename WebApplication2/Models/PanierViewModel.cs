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

        public IList<Experience> experiences  { get; set; }
        public IList<ServiceLogment> Logments { get; set; }
        public IList<ServiceNouritture> nourittures { get; set; }
        public IList<ServiceTransport> Transports { get; set; }


    }
}
