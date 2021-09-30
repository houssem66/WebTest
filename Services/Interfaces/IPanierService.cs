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

        public Task Update(Panier panier, ServiceLogment serviceLogment);
        public Task UpdatePanier(Panier panier);
        public Task<Panier> GetPanierById(int id);
        public IEnumerable<Panier> GetPanierByuserId(string id);
        public decimal PrixTotal(Panier panier, int nbrNuit, int nbrRepats, int nbrJours);
        public Task InsertAsync(Panier reservation, int id);
        public Task<Panier> GetPanier(int id);

        public Task<Panier> GetPan(int id);
        public Task UpdateNourriture(Panier panier, ServiceNouritture serviceNouritture);
        public Task UpdateTransport(Panier panier, ServiceTransport serviceTransport);
        public Task<Panier> GetPanUser(string id);
        public IList<Panier> GetPanierByUser(string id);



    }
}
