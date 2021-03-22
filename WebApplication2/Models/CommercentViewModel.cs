using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace WebApplication2.Models
{
    public class CommercentViewModel
    {

        public string Type { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public string PreNom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

       
        [Display(Name = "Domaine d'activité")]
        public string Domaine { get; set; }
        [Display(Name = "Forme Juridique")]
        public string Forme { get; set; }

        public int EffectFemme { get; set; }
        public int EffectHomme { get; set; }
        [Display(Name = "Situation Micro-Entreprise")]
        public string SituationEntreprise { get; set; }
        public string NomGerant { get; set; }
        public string Patente { get; set; }
        [Display(Name = "Secteur")]
        public string Secteur { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }




        [Phone]
        public string Telephone { get; set; }
        [Display(Name = "Phone number country")]
        public string PhoneNumberCountryCode { get; set; }


    }
}
