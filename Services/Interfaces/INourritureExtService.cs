using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
   public interface INourritureExtService
    {
        public Task Ajout(ServiceNouritture logement);
        public IList<ServiceNouritture> GetAllLogements();
        public Task Update(ServiceNouritture logement);
        public Task<ServiceNouritture> GetLogementById(int id);
        public Task Delete(ServiceNouritture logement);
        public IList<ServiceNouritture> GetNourritureByUser(string id);
        public Task<ServiceNouritture> GetById(int id);
    }
}
