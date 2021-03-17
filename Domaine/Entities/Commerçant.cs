using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TourMe.Data.Entities.Enum;

namespace Domaine.Entities
{
   public  class Commerçant:Utilisateur
    {
        
        public int CIN { get; set; }
       
        public domaine DomainActivite { get; set; }
        public string FormeJuridique { get; set; }
        public int EffectFemme { get; set; }
        public int EffectHomme { get; set; }
        public string SituationEntreprise { get; set; }
        public string NomGerant { get; set; }
        public string Patente { get; set; }
        public Secteur Secteur { get; set; }

    }
}
