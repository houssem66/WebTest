using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
 public   interface IPanierRepo
    {
        public Task Update(Panier panier, ServiceLogment serviceLogment);
        public Task UpdatePanier(Panier panier);
        public Task UpdateNourriture(Panier panier, ServiceNouritture serviceNouritture); 
        public Task UpdateTransport(Panier panier, ServiceTransport serviceTransport);
        public decimal PrixTotal(Panier panier, int nbrNuit, int nbrRepats, int nbrJours);
        public IEnumerable<Panier> GetPanierByuserIdAsync(string id);
        public Task<Panier> GetPanier(int id);
        public Task<Panier> GetPan(int id);
        Task InsertAsync(Panier panier, int id);
        public Task<Panier> GetPanUser(string id);
        public IQueryable<Panier> GetAllpanierAsync();
    }
}
