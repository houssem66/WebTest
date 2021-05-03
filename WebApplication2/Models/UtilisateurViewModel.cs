using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class UtilisateurViewModel
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Telephone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }



        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string ReturnUrl { get; set; }
        public IFormFile Photo { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        [Display(Name = "Phone number country")]
        public string PhoneNumberCountryCode { get; set; }
        public string ExistingPhotoPath { get; set; }
        public String Adresse { get; set; }
        public string carte { get; set; }
        public string NickName { get; set; }
        public string Country { get; set; }
        public string role { get; set; }

    }
}
