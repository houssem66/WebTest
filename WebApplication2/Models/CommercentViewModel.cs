using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TourMe.Data.Entities.Enum;

namespace TourMe.Web.Models
{
    public class CommercentViewModel
    {
        public string TypseService { get; set; }
        [Required]
        public string PersAContact { get; set; }
        public string Type { get; set; }
        public string TypeOrgan { get; set; }

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
        [RegularExpression(@"^[0-9]{8}[A-Za-z]$", ErrorMessage = "Must be a In this format 12345678X.")]
        [StringLength(9, ErrorMessage = "The {0}  cannot exceed {1} characters. ")]
        public string Identifiant_fiscale { get; set; }
        [Display(Name = "Secteur")]
        public string Secteur { get; set; }
        public string Titre { get; set; }

        [Required(ErrorMessage = "Password est obligatoire")]
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
        public IFormFile FileP { get; set; }
        public List<IFormFile> Documents { get; set; }
       
        public int CodePostale { get; set; }
        public Region region { get; set; }
        public string Adresse { get; set; }
       
        public int NumPersAcontacter { get; set; }
        public long NumCnss { get; set; }

    }
}
