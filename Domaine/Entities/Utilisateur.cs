using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using TourMe.Data.Entities.Enum;
using TourMe.Data.Entities;

namespace Domaine.Entities
{
    public class Utilisateur : IdentityUser
    {
        public string NickName { get; set; }
        public Region region { get; set; }
        public string carte { get; set; }

        public String Nom { get; set; }

        public String Prenom { get; set; }
        public string Country { get; set; }


        public int? Telephone { get; set; }
        public String ProfilePhoto { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public String Adresse { get; set; }
        public string Interet { get; set; }
        public Gender gender { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }


    }
}
