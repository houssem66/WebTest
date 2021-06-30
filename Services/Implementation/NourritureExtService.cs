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
    public class NourritureExtService : INourritureExtService
    {
        private readonly IGenericRepository<ServiceNouritture> GenericRepo;
        private readonly INourritureExtRepo nourritureExtRepo;

        public NourritureExtService(IGenericRepository<ServiceNouritture> genericRepo, INourritureExtRepo nourritureExtRepo)
        {
          GenericRepo = genericRepo;
            this.nourritureExtRepo = nourritureExtRepo;
        }
        public Task Ajout(ServiceNouritture logement)
        {
            return GenericRepo.InsertAsync(logement);
        }

        public Task Delete(ServiceNouritture logement)
        {
            return GenericRepo.DeleteAsync(logement.Id);
        }

        public IList<ServiceNouritture> GetAllLogements()
        {
            return nourritureExtRepo.GetAll().ToList();
        }

        public Task<ServiceNouritture> GetLogementById(int id)
        {
            return GenericRepo.GetByIdAsync(id);
        }

        public Task Update(ServiceNouritture logement)
        {
            return GenericRepo.PutAsync(logement.Id, logement);
        }

        public IList<ServiceNouritture> GetNourritureByUser(string id)
        {
            return GenericRepo.GetAll().Where(e => e.FournisseurId.Equals(id)).ToList();

        }

        public async  Task<ServiceNouritture> GetById(int id)
        {
            ServiceNouritture l = await nourritureExtRepo.GetNourritureDetailsAsync(id);
            return l;


        }
    }
}
