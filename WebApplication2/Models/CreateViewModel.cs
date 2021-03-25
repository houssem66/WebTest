using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class CreateViewModel
    {[Required]
        public string RoleName { get; set; }
    }
}
