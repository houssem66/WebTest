using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
   public interface IPanierService
    {
        public Task Ajout(Panier panier);

        public Task Update(Panier panier);
        public Task<Panier> GetPanierById(int id);
        public IEnumerable<Panier> GetPanierByuserId(string id);
        public decimal PrixTotal(Panier panier, int nbrNuit, int nbrRepats, int nbrJours);



    }
}
