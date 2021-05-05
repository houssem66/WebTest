using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Web.Models
{
    public class LogementExtViewModel
    {
       
        public Category Category { get; set; }
        public string Images { get; set; }
         public IFormFile Documents { get; set; }
        public decimal PrixParNuit { get; set; }
        public string Adresse { get; set; }
        public string Titre { get; set; }
    }
}
