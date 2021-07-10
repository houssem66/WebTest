using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Plantes
    {
        [Display(Name = "Agriculture")] a,
        [Display(Name = "Botanique")]z ,
        [Display(Name = "Phytothérapie")] e,
        [Display(Name = "Jardinage")]r ,
        [Display(Name = "Autres plantes et agriculture")] t,
    }
}
