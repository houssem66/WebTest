using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace TourMe.Web.Models
{
    public class HomeViewModel
    {
        public List<ServiceNouritture> ListeNourriture { get; set; }
        public List<ServiceLogment> ListeLogement { get; set; }
        public List<Experience> ListeExperience { get; set; }
    }
}
