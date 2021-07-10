using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Food
    {
        [Display(Name = "Cuisine et alimentation ")]a ,
        [Display(Name = "Dégustation gastronomique ")] z,
        [Display(Name = "Diner en groupe ")] e,
        [Display(Name = "Visite de marché et gastronomie ")] zz,
        [Display(Name = "Autre Expérience culinaire ")]ee ,

    }
}
