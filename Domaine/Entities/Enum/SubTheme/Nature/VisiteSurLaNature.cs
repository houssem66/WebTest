using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum VisiteSurLaNature
    {
        [Display(Name = "Excursion dans une baie")]a ,
        [Display(Name = "Excursion dans une cénote")]z ,
        [Display(Name = "Excursion à la campagne")] e,
        [Display(Name = "Excursion en forêt")]r ,
        [Display(Name = "Excursion dans une jungle")] t,
        [Display(Name = "Parc national")]y ,
        [Display(Name = "Excursion sur une rivière")] u,
        [Display(Name = "Autre visite sur la nature")] i,
        [Display(Name = "Visite d'une grotte")]o ,
        [Display(Name = "Excursion côtière")]p ,
        [Display(Name = "Excursion dans un désert")] q,
        [Display(Name = "Visite de source d'eau chaude")]s ,
        [Display(Name = "Excursion sur un lac")] d,
        [Display(Name = "Excursion sur l'océan")]f ,
        [Display(Name = "Excursion sur un volcan")] g,
    }
}
