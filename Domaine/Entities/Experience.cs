using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public class Experience
    {
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal tarif { get; set; }
        public int ExperienceId { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string TypeExperience { get; set; }
        [Required]
        public string Lieu { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime dateDebut { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime dateFin { get; set; }
        public string Saison { get; set; }
        public string ImagesExperience { get; set; }
        public string Activité { get; set; }
        public string Rating { get; set; }
        
        public int NbPlaces { get; set; }
        public string AvgRating { get; set; }
        //navigation Property
        public virtual ICollection<Commentaire> Commentaires { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual IList<Activite> Activites { get; set; }
    }
}
