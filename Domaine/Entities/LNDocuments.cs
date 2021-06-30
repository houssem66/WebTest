using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public  class LNDocuments
    {
        public int Id { get; set; }
        public string Filepath { get; set; }
     
        public virtual ServiceLogment ServiceLogment { get; set; }
        public virtual ServiceNouritture ServiceNouritture { get; set; }
 
    }
}
