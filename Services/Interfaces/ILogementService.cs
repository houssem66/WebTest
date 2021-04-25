using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
   public interface ILogementService
    {
        public Task Ajout(Logement logement);
        public IList<Logement> GetAllLogements();
        public Task Update(Logement logement);
        public Task<Logement> GetLogementById(int id);
        public Task Delete(Logement logement);
    }
}
