using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using TourMe.Data.Entities.Enum;

namespace TourMe.Web.Models
{
    public class ExperienceViewModel
    {
        [Required(ErrorMessage = "Tarif est obligatoire")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [DisplayName("Tarif (dt)")]
        public decimal tarif { get; set; }
        public int ExperienceId { get; set; }
        public int ActiviteId { get; set; }

        [Required]
        [MinLength(2)]
        public string Titre { get; set; }
        [Required]
  
        public TypeExperience TypeExperience { get; set; }
        [Required]
        public string Lieu { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateDebut { get; set; }
        [DataType(DataType.Date)]
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
