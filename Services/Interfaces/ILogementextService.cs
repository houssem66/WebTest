using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
   public interface ILogementextService
    {
        public Task Ajout(ServiceLogment logement);
        public IList<ServiceLogment> GetAllLogements();
        public Task Update(ServiceLogment logement);
        public Task<ServiceLogment> GetLogementById(int id);
        public Task Delete(ServiceLogment logement);
        public  Task<ServiceLogment> GetById(int id);
    }
}
