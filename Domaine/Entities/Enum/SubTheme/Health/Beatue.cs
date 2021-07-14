using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Beatue
    {
        [Display(Name = "Consultation sur les parfums")]a ,
        [Display(Name = "Cours sur les soins des cheveux")]z,
        [Display(Name = "Traitement et soin des cheveux")]e ,
        [Display(Name = "Cours de maquillage")]r ,
        [Display(Name = "Atelier maquillage")]t ,
        [Display(Name = "Consultation en soins de la peau")] y,
        [Display(Name = "Atelier soins de la peau")]u ,
        [Display(Name = "Atelier parfumerie")]i ,
        [Display(Name = "Consultation sur les soins des cheveux")] az,
        [Display(Name = "Atelier soins des cheveux")]ae ,
        [Display(Name = "Consultation maquillage")]ar ,
        [Display(Name = "Cours sur les soins de la peau")] at,
        [Display(Name = "Traitement de la peau")] ze,
        [Display(Name = "Autre expérience beauté")]er ,
    }
}
