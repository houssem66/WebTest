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
       
        public long NumCnss { get; set; }
        public int NumPersAcontacter { get; set; }
        public TypeService TypeService { get; set; }
        public IList<ServiceLogment> ServiceLogments { get; set; }
        public IList<ServiceNouritture> ServiceNourittures { get; set; }
        public IList<ServiceTransport> ServiceTransports { get; set; }
        


    }
}
