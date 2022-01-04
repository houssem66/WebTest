using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
    public interface IHebergementService
    {
        public Task Ajout(Hebergement entity);
        public Logement GetHebergement(int id);
        public Task Update(Hebergement entity);
        public Task<Hebergement> GetHebergementById(int id);
        public Task DeleteHebergementAsnc(int id);
    }
}
