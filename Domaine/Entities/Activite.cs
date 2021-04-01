using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public class Activite
    {
        public int activiteId { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public int ExperienceId { get; set; }
    }
}
