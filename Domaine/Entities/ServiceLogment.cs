using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Data.Entities
{
  public  class ServiceLogment
    {[Key]
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Images{ get; set; }
        public string Documents { get; set; }
        public decimal PrixParNuit { get; set; }
        public string Adresse { get; set; }
        public string Titre { get; set; }


        public Fournisseur Fournisseur { get; set; }
    }
}
