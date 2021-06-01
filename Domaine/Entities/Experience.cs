using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Data.Entities
{
   public class Experience
    {
        [Required(ErrorMessage = "Tarif est obligatoire")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [DisplayName("Tarif (dt)")]
        public decimal tarif { get; set; }
        public int ExperienceId { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public TypeExperience TypeExperience { get; set; }
        public SubExperience SubExperience { get; set; }
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
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual IList<Activite> Activites { get; set; }

        //public virtual IList<Nourriture> Nourritures { get; set; }
        //  public virtual IList<Logement> Logements { get; set; }
        public Logement Logement { get; set; }
        public Nourriture Nourriture { get; set; }
        public Transport Transport { get; set; }
        public string CommerçantId { get; set; }

    }
}
