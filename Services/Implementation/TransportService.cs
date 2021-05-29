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
    public class TransportService : ITransportService
    {

        private readonly IGenericRepository<Transport> GenericRepo;
        readonly private ITransportRepo TransportRepo;
        public TransportService(IGenericRepository<Transport> genericRepository, ITransportRepo transportRepo)
        {
            GenericRepo = genericRepository;
            TransportRepo = transportRepo;

        }

        public Task Ajout(Transport entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public Transport GetTransport(int id)
        {
            var a = GenericRepo.GetAll().Where(e => e.ExperienceId == id).ToList().First();
            return a;
        }

        public async Task<Transport> GetTransportById(int id)
        {
            Transport a = await GenericRepo.GetByIdAsync(id);
            return a;
        }

        public Task Update(Transport entity)
        {
            return TransportRepo.Update(entity);
        }
    }
}