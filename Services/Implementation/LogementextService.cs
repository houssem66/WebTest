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
   public class LogementextService:ILogementextService
    {
        private readonly IGenericRepository<ServiceLogment> GenericRepo;
        private readonly ILogementextRepo LogementRepo;

        public LogementextService(IGenericRepository<ServiceLogment> genericRepo, ILogementextRepo logementRepo)
        {
            GenericRepo = genericRepo;
            LogementRepo = logementRepo;
        }

        public  Task Ajout(ServiceLogment logement)
        {
           return  GenericRepo.InsertAsync(logement);
        }

        public Task Delete(ServiceLogment logement)
        {
            return GenericRepo.DeleteAsync(logement.Id);
        }

        public IList<ServiceLogment> GetAllLogements()
        {
            return LogementRepo.GetAll().ToList();
        }

        public async  Task<ServiceLogment> GetById(int id)
        {
            ServiceLogment l = await LogementRepo.GetlogementDetailsAsync(id);
            return l;




         
        }

        public Task<ServiceLogment> GetLogementById(int id)
        {
            return GenericRepo.GetByIdAsync(id);
        }

        public Task Update(ServiceLogment logement)
        {
            return GenericRepo.PutAsync(logement.Id,logement);
        }
    }
}
