using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
   public interface ILogementextRepo
    {
        public Task<ServiceLogment> GetlogementDetailsAsync(int id);
        public IQueryable<ServiceLogment> GetAll();

    }
}
