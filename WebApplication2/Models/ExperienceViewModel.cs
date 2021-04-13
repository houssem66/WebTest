using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace TourMe.Web.Models
{
    public class ExperienceViewModel
    {
        public int ExperienceId { get; set; }
        public int ActiviteId { get; set; }

        [Required]
        public string Titre { get; set; }
        [Required]
        public string TypeExperience { get; set; }
        [Required]
        public string Lieu { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string Saison { get; set; }
        public string ImagesExperience { get; set; }
        public string Activité { get; set; }
        public string AvGRating { get; set; }

        public int NbPlaces { get; set; }
        public List<IFormFile> FileP { get; set; }
        // public List<Activite> Activites { get; set; }
        public ICollection<Activite> Activites;
    }
}
