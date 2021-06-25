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
        public string Category { get; set; }
        public string Type { get; set; }
     

        public decimal PrixParNuit { get; set; }
        public string Adresse { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }


        public Fournisseur Fournisseur { get; set; }
        public virtual IList<LNDocuments> Documents { get; set; }
    }
}
