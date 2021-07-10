using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum CielNocturne
    {
        [Display(Name = "Photographie de ciel nocturne")]a ,
        [Display(Name = "Astronomie")]z ,
        [Display(Name = "Observation des étoiles")]e ,
        [Display(Name = "Autre expérience sur le ciel nocturne")]r ,
    }
}
