using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum EtatEsprit
    {
        [Display(Name = "Coaching en body positivisme")] a,
        [Display(Name = "Coaching développement personnel")]z ,
        [Display(Name = "Coaching en relations et en sexualité")]e ,
        [Display(Name = "Autre expérience sur l'état d'esprit")] r,
        [Display(Name = "Atelier body positive")]t ,
        [Display(Name = "Atelier développement personnel")]az ,
        [Display(Name = "Atelier sur les relations et la sexualité")]ae ,
    }
}
