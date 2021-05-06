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
       
        public TypeNourriture Type { get; set; }
        public string Plat { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Prix est obligatoire")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [DisplayName("Tarif (dt)")]
        public decimal Prix { get; set; }
    }
}
