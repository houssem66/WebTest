using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
  public  class FournisseurService :IFournisseurService
    {
        private readonly IGenericRepository<Fournisseur> genericRepo;
        private readonly IFournisseurRepo fournisseurRepo;

        public FournisseurService(IGenericRepository<Fournisseur> genericRepo, IFournisseurRepo FournisseurRepo)
        {
            this.genericRepo = genericRepo;
            fournisseurRepo = FournisseurRepo;
        }

        public Task Ajout(Fournisseur fournisseur)
        {
            return genericRepo.InsertAsync(fournisseur);
        }

        public async Task<string> Delete(Fournisseur fournisseur)
        {
            var ch = await fournisseurRepo.Delete(fournisseur);
            return ch;
        }

        public IList<Fournisseur> GetAllFournisseurs()
        {
            return genericRepo.GetAll().ToList();
        }

        public Task<Fournisseur> GetFournisseurById(string id)
        {
            return genericRepo.GetByIdAsync(id);
        }

        public Task Update(Fournisseur fournisseur)
        {
            return genericRepo.PutAsync(fournisseur.Id, fournisseur);
        }
    }
}
