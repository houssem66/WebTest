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
   public class Logement
    {
        public int Id { get; set; }
        
        public string  Titre { get; set; }
        public string Description { get; set; }
        public decimal PrixParNuit { get; set; }
        public int Adresse { get; set; }
        public string Photo { get; set; }
        public int NbrNuit { get; set; }
        public int ExperienceFk { get; set; }
        public Experience Experience { get; set; }

    }
}
