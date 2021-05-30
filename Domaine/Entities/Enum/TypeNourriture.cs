using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
    public enum TypeNourriture
    {Traditionel, [Display(Name = "La cuisine gastronomique")] LaCuisine, [Display(Name = "La cuisine Exotic")] Exotic, [Display(Name = "La nouvelle cuisine")] nouvelle, [Display(Name = "La cuisine moléculaire")] moléculaire
    }
}
