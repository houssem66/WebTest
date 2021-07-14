using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public  enum Event
    {
        [Display(Name = "Sport ")] f,
        [Display(Name = "Divertissement ")] d,
        [Display(Name = "Autre Type d' Evénement ")] a,

    }
}
