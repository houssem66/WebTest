using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
    public interface INourritureExtRepo
    {
        public Task<ServiceNouritture> GetNourritureDetailsAsync(int id);
        public IQueryable<ServiceNouritture> GetAll();
    }
}
