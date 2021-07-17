using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class ActiviteViewModel
    {
        public string Titre { get; set; }
        public IFormFile FileP { get; set; }
        public string Details { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime dateDebut { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-ddThh:mm}")]
        public DateTime dateFin { get; set; }
        public int Index { get; set; }
        public string Duree { get; set; }
    }
}
