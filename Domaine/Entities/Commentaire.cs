using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public class Commentaire
    {
        [Required]
        public int CommentaireId { get; set; }
        public string Details { get; set; }
        public int ExperienceId { get; set; }
   
      
    }
}
