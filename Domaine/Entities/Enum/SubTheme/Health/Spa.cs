using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public  enum Spa
    {
        [Display(Name = "Rituel en spa")]a ,
        [Display(Name = "Traitement en spa")] z,
        [Display(Name = "Visite de spa")] e,
        [Display(Name = "Atelier spa")] r,
    }
}
