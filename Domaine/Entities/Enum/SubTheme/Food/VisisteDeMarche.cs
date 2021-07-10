using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum VisisteDeMarche
    {
        [Display(Name = "Visite d'une ferme")]a ,
        [Display(Name = "Visite de marché")] b,
        [Display(Name = "Randonnées-cueillette")]c ,
        [Display(Name = "Visite sur le thême de la cuisine de rue")] d,
        
    }
}
