using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Web.Models
{
    public class NourritureExtViewModel
    {
        public string Rating { get; set; }
        public string SpecialeResto { get; set; }
        public string NomRestau { get; set; }
        public string TypeResto { get; set; }
        public List<IFormFile> FileP { get; set; }       
        public IFormFile FilePp { get; set; }
        public string regles { get; set; }

        public string Plat { get; set; }
        public string Image { get; set; }
        public string Slogon { get; set; }
        public string Site { get; set; }
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime dateOuvert { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime dateFerme { get; set; }
        public string Adresse { get; set; }
        [Required(ErrorMessage = "Prix est obligatoire")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [DisplayName("Tarif (dt)")]
        public decimal Prix { get; set; }
    }
}
