using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Cuisine
    {
        [Display(Name = "Cours de pâtisserie")] a,
        [Display(Name = "cours de mise en conserve")] z,
        [Display(Name = "Atelier de fabrication de chocolat")] e,
        [Display(Name = "Cours sur la fermenation")] t,
        [Display(Name = "Autre cours de cuisine")] y,
        [Display(Name = "Cours sur la fabrication des bonbons")] u,
        [Display(Name = "cours sur la fabrication de frommage")] i,
        [Display(Name = "Cours de cuisine")] o,
        [Display(Name = "Cours sur le maniement de couteaux")] p,
    }
}
