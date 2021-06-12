using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TourMe.Data.Entities;
using TourMe.Data.Entities.Enum;

namespace TourMe.Data.Entities
{
  public class Fournisseur :Commerçant
    {
       [RegularExpression(@"([0-9]+)", ErrorMessage = "Must be a Number.")]
        [StringLength(10, ErrorMessage = "The {0}  cannot exceed {1} characters. ")]
        public string NumCnss { get; set; }
        public TypeService TypeService { get; set; }
        public IList<ServiceLogment> ServiceLogments { get; set; }
        public IList<ServiceNouritture> ServiceNourittures { get; set; }
        public IList<ServiceTransport> ServiceTransports { get; set; }
        


    }
}
