using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Nature
    {
        [Display(Name = "Camping ")]a ,
        [Display(Name = "Randoneé ")] z,
        [Display(Name = "Voyage en sac à dos ")] e,
        [Display(Name = "Ciel nocturne ")] r,
        [Display(Name = "Visite sur la nature et l'écologie ")]t ,
        [Display(Name = "Plantes et agriculture ")]az ,
        [Display(Name = "Activité plein air ")] ae,
        [Display(Name = "Autre Expérience Naturelle ")]ar ,

    }
}
