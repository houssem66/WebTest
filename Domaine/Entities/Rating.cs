using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domaine.Entities;

namespace TourMe.Data.Entities
{
    public class Rating
    {
        public string UtilisateurId { get; set; }
        public virtual Utilisateur utilisateur { get; set; }

        public int ExperienceId { get; set; }
        public virtual Experience experience { get; set; }
        
        public string note { get; set; }

        public string Commentaire { get; set; }

    }
}
