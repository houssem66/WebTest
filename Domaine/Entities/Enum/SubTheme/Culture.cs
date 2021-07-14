using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Culture
    {
        [Display(Name = "Cours sur l’entrepreneuriat ")] a,
        [Display(Name = "Conférence culturelle ")] z,
        [Display(Name = "Cours de langue ")] e,
        [Display(Name = "Visite d’usine ")] ae,
        [Display(Name = "Visite de campagne ")] az,
        [Display(Name = "Autre activité culturelle ")] aaz,
        [Display(Name = "Cours de sciences")] adz,
        [Display(Name = "Conférence sur des enjeux sociaux")] daza,
        [Display(Name = "Danse culturelle")] ddd,
        [Display(Name = "Visite culturelle")] qsd,
        [Display(Name = "Visite de bureau")] sd,
        [Display(Name = "Festival Culturelle")] f,


    }
}
