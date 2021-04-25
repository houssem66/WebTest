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
   public class LogementService:ILogementService
    {
        private readonly IGenericRepository<Logement> GenericRepo;
        private readonly ILogementRepo LogementRepo;

        public LogementService(IGenericRepository<Logement> genericRepo, ILogementRepo logementRepo)
        {
            GenericRepo = genericRepo;
            LogementRepo = logementRepo;
        }

        public  Task Ajout(Logement logement)
        {
           return  GenericRepo.InsertAsync(logement);
        }

        public Task Delete(Logement logement)
        {
            return GenericRepo.DeleteAsync(logement.Id);
        }

        public IList<Logement> GetAllLogements()
        {
            return GenericRepo.GetAll().ToList();
        }

        public Task<Logement> GetLogementById(int id)
        {
            return GenericRepo.GetByIdAsync(id);
        }

        public Task Update(Logement logement)
        {
            return GenericRepo.PutAsync(logement.Id,logement);
        }
    }
}
