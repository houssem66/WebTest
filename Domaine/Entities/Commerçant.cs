using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TourMe.Data.Entities.Enum;

namespace Domaine.Entities
{
   public  class Commerçant:Utilisateur
    {


        public string PersAContact { get; set; }
        public string Type { get; set; }
        public string DomainActivite { get; set; }
        public string FormeJuridique { get; set; }
        public int EffectFemme { get; set; }
        public int EffectHomme { get; set; }
        public string SituationEntreprise { get; set; }
        public string NomGerant { get; set; }
        public string Patente { get; set; }
        public string Secteur { get; set; }
        public string TypeOrgan { get; set; }
      
    }
}
