using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
   public class EmployeDocuments
    {
        public int Id { get; set; }
        public string Filepath { get; set; }
        public Commerçant Commerçant { get; set; }
    }
}
