using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
    public enum SubCategory
    {Maison,Appartement,Hotel, [Display(Name = "Maison d'hote")]maisonhote
    }
}
