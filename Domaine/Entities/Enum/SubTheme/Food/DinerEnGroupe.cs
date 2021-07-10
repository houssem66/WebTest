using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum DinerEnGroupe
    {
        [Display(Name = "Barbacue")]a ,
        [Display(Name = "Brunch")] z,
        [Display(Name = "Petit Dêjuner")] e,
        [Display(Name = "Dîner")] r,
        [Display(Name = "Déjuner")] t,
        [Display(Name = "Pique-nique")] y,
        [Display(Name = "Autre experience gastronomique")] u,
    }
}
