using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
   public interface IHôteRepo
    {
        public IQueryable<Commerçant> GetAllCommercant();
        public IEnumerable<EmployeDocuments> GetListfile(string id);
    }
}
