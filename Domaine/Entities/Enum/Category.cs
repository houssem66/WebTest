﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
  public  enum Category
    {
        [Display(Name = "Logement Entier")] LogementEntier, [Display(Name = "Chambre privée")] Chambreprivé, [Display(Name = "Chambre partagée")]ChambrePartagé
    }
}
