using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Sport
    {
        [Display(Name = "Sport d'équipe")] SportDequipe,
        [Display(Name = "Sport de combat")] SportDecombat,
        [Display(Name = "Sports d'extérieur")] SportsDextérieur,
        [Display(Name = "Sport de montagne")] SportDeMontagne,
        [Display(Name = "Sports d'adrénaline")] Sportsdadrénaline,
        [Display(Name = "Fitness")] Fitness,
        [Display(Name = "Sport d'hiver")] SportDhiver,
        [Display(Name = "Sport Aquatiques")] SportAquatiques,
        [Display(Name = "Autre Sport")] AutreSport,

    }
}
