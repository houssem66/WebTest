using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Data.Entities
{
  public class Fournisseur :Commerçant
    {
        public TypeService TypeService { get; set; }
        public IList<ServiceLogment> ServiceLogments { get; set; }
        public IList<ServiceNouritture> ServiceNourittures { get; set; }


    }
}
