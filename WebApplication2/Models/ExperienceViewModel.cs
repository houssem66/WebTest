using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class ExperienceViewModel
    {
        public string ExperienceId { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string TypeExperience { get; set; }
        [Required]
        public string Lieu { get; set; }
        public string dateDebut { get; set; }
        public string dateFin { get; set; }
        public string Saison { get; set; }
        public string ImagesExperience { get; set; }
        public string Activité { get; set; }
        public string Rating { get; set; }

        public int NbPlaces { get; set; }
    }
}
