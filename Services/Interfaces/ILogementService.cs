using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
    public interface ILogementService
    {
        public Task Ajout(Logement entity);
        public Logement GetLogement(int id);
        public Task Update(Logement entity);
        public Task<Logement> GetLogementById(int id);
    }
}
