using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
    public enum TypeTransport
    {
        [Display(Name = "Transport Touristique")] touristique, [Display(Name = "Transport Privé")] Privé, [Display(Name = "Transport Occasionnel")]occasionele
    }
}
