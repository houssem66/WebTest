using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public class Reservation
    {
        public string UtilisateurId { get; set; }
        public virtual Utilisateur utilisateur { get; set; }

        public int ExperienceId { get; set; }
        public virtual Experience experience { get; set; }

        public DateTime date { get; set; }
        public int NbrReservation { get; set; }
        public decimal tariff { get; set; }
        public int MyProperty { get; set; }
    }
}
