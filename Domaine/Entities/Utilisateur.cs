using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domaine.Entities
{
    public class Utilisateur :IdentityUser
    {
        public int carte { get; set; }
       
        public String Nom { get; set; }
      
        public String Prenom { get; set; }
        
       
        public int? Telephone { get; set; }
        public String ProfilePhoto { get; set; }
       
        public String BirthDate { get; set; }
        public String Adresse { get; set; }

    }
}
