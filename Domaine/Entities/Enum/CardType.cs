using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum
{
  public enum CardType
    {
        [Display(Name = "Master Card")]
        MasterCard,
        visa ,
        [Display(Name = " American Express")]
        AmericanExpress

       
        
    }
}
