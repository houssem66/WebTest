using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
    public class PanierService : IPanierService
    {
        private readonly IGenericRepository<Panier> genericRepo;
        private readonly IPanierRepo panierRepo;


        public PanierService(IGenericRepository<Panier> genericRepo, IPanierRepo panierRepo)
        {
            this.genericRepo = genericRepo;
            this.panierRepo = panierRepo;

        }
        public Task Ajout(Panier panier)
        {

            return genericRepo.InsertAsync(panier);

        }

        public Task<Panier> GetPanierById(int id)
        {
            var panier = genericRepo.GetByIdAsync(id);
            return panier;
        }

        public IEnumerable<Panier> GetPanierByuserId(string id)
        {

            return panierRepo.GetPanierByuserIdAsync(id);
        }

        public decimal PrixTotal(Panier panier, int nbrNuit, int nbrRepats, int nbrJours)
        {
            return panierRepo.PrixTotal(panier, nbrNuit, nbrRepats, nbrJours);
        }

        public async Task Update(Panier panier, ServiceLogment serviceLogment)
        {
            await panierRepo.Update(panier, serviceLogment);
        }
        public Task InsertAsync(Panier reservation, int id)
        {
            return panierRepo.InsertAsync(reservation, id);
        }

        public Task<Panier> GetPanier(int id)
        {
            return panierRepo.GetPanier(id);
        }

        public Task<Panier> GetPan(int id)
        {
            return panierRepo.GetPan(id);
        }

        public Task UpdateNourriture(Panier panier, ServiceNouritture serviceNouritture)
        {
            return panierRepo.UpdateNourriture(panier, serviceNouritture);
        }

        public Task UpdateTransport(Panier panier, ServiceTransport serviceTransport)
        {
            return panierRepo.UpdateTransport(panier, serviceTransport);
        }

        public Task<Panier> GetPanUser(string id)
        {
            return panierRepo.GetPanUser(id);
        }

        public Task UpdatePanier(Panier panier)
        {
           return  panierRepo.UpdatePanier(panier);
        }

       
        public IList<Panier> GetPanierByUser(string id)
        {
            return panierRepo.GetAllpanierAsync().Where(e => e.User.Id.Equals(id)).ToList();

        }
    }
}