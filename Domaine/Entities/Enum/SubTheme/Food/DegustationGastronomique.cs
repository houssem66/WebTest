using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum DegustationGastronomique
    {
        [Display(Name = "Dégustation d'huiles d'olive")] a,
        [Display(Name = "Dégustation de chocolats")]z ,
        [Display(Name = "Dégustation de fromages")] e,
        [Display(Name = "Autre dégustation gastronomique")]r ,
    }
}
