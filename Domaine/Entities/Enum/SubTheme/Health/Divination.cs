using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Divination
    {
        [Display(Name = "Cours d'astrologie")] a,
        [Display(Name = "Atelier astrologie")] z,
        [Display(Name = "Atelier sur l'interprétation des rêves")] e,
        [Display(Name = "Atelier voyance")] r,
        [Display(Name = "Autre expérience de divination")] t,
        [Display(Name = "Consultation d'astrologie")] az,
        [Display(Name = "Consultation sur l'interprétation des rêves")] ae,
        [Display(Name = "Consultation de voyance")]ar ,
        [Display(Name = "Tirage de cartes de tarot")]ze ,
    }
}
