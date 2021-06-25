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
   public class TransportExtService : ITransportExtService
    {
        private readonly IGenericRepository<ServiceTransport> GenericRepo;
        private readonly ITransportExtRepo transportExtRepo;

        public TransportExtService(IGenericRepository<ServiceTransport> genericRepo, ITransportExtRepo transportExtRepo)
        {
            GenericRepo = genericRepo;
            this.transportExtRepo = transportExtRepo;
        }
        public Task Ajout(ServiceTransport logement)
        {
            return GenericRepo.InsertAsync(logement);
        }

        public Task Delete(ServiceTransport logement)
        {
            return GenericRepo.DeleteAsync(logement.Id);
        }

        public IList<ServiceTransport> GetAllLogements()
        {
            return GenericRepo.GetAll().ToList();
        }

        public Task<ServiceTransport> GetLogementById(int id)
        {
            return GenericRepo.GetByIdAsync(id);
        }

        public Task Update(ServiceTransport logement)
        {
            return GenericRepo.PutAsync(logement.Id, logement);
        }
    }
}
