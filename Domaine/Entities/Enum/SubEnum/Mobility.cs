using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
    public enum Mobility
    {
        [Display(Name = "Transport privé")] Transportprivé,Logistique,
        [Display(Name = "Location automobile")] Locationautomobile,autre
    }
}
