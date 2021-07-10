using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum SanteHolistique
    {
        [Display(Name = "Atelier nutrition")] a,
        [Display(Name = "Traitement ayurveda")]z ,
        [Display(Name = "Cours de nutrition")]e ,
        [Display(Name = "Traitement en phytothérapie")]r ,
        [Display(Name = "Consultation en nutrition")]t ,
        [Display(Name = "Traitement en médecine traditionnelle chinoise")] y,
        [Display(Name = "Autre expérience en santé holistique")] g,
        [Display(Name = "Cours d'ayurveda")] az,
        [Display(Name = "Atelier ayurveda")]ae ,
        [Display(Name = "Cours de phytothérapie")] ar,
        [Display(Name = "Cours de médecine traditionnelle chinoise")]at ,
        [Display(Name = "Atelier phytothérapie")] f,
        [Display(Name = "Atelier médecine traditionnelle chinoise")]d ,
    }
}
