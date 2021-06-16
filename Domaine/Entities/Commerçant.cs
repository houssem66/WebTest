using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TourMe.Data.Entities;
using TourMe.Data.Entities.Enum;

namespace Domaine.Entities
{
   public  class Commerçant:Utilisateur
    {


        public string PersAContact { get; set; }
        [DefaultValue(false)]
        public Boolean Verified { get; set; }
        public string Type { get; set; }
        public string DomainActivite { get; set; }
        public string FormeJuridique { get; set; }
        public int EffectFemme { get; set; }
        public int EffectHomme { get; set; }
        public string SituationEntreprise { get; set; }
        public string NomGerant { get; set; }
        [RegularExpression(@"^[0-9]{8}[A-Za-z]$", ErrorMessage = "Must be a In this format 12345678X.")]
        [StringLength(9, ErrorMessage = "The {0}  cannot exceed {1} characters. ")]
        public string Identifiant_fiscale { get; set; }
        public string Patente { get; set; }
       
        public string Secteur { get; set; }
        public string TypeOrgan { get; set; }
        public string Titre { get; set; }
        //personne physique
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Must be a Number.")]
        [StringLength(8, ErrorMessage = "The {0}  cannot exceed {1} characters. ")]
        public string Cin { get; set; }
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Must be a Number.")]
        [StringLength(20, ErrorMessage = "The {0}  cannot exceed {1} characters. ")]
        public int Rib { get; set; }
        // ajouté le 11/06/2021
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Must be a Number.")]
        [StringLength(4, ErrorMessage = "The {0}  cannot exceed {1} characters. ")]
        public int CodePostale { get; set; }
        public virtual IList<Experience> Experiences { get; set; }
        public virtual IList<EmployeDocuments> EmployeDocuments{ get; set; }
    }
}
