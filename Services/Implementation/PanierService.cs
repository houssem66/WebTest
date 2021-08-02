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
        public async Task Ajout(Panier panier)
        { 
            
            await genericRepo.InsertAsync(panier);
          
        }

        public Task<Panier> GetPanierById(int id)
        {
            var panier = genericRepo.GetByIdAsync(id);
            return panier;
        }

        public Task<Panier> GetPanierByuserId(string id)
        {
            var panier = genericRepo.GetAll().Where(e => e.UserId == id);
            return (Task<Panier>)panier;
        }

        public decimal PrixTotal(Panier panier, int nbrNuit, int nbrRepats, int nbrJours)
        {
            return panierRepo.PrixTotal(panier, nbrNuit, nbrRepats, nbrJours);
        }

        public async Task Update(Panier panier)
        {
           await panierRepo.Update(panier);
        }
    }
}
