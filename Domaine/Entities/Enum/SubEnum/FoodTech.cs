using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
    public enum FoodTech
    {
        [Display(Name = "Food Tech")] foodtech, [Display(Name = "New Food")] NewFood,Restauration,autre
    }
}
