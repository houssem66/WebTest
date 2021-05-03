using Repository.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
    public class LogementService : ILogementService
    {
        private readonly IGenericRepository<Logement> GenericRepo;
        public LogementService(IGenericRepository<Logement> genericRepo)
        {
            GenericRepo = genericRepo;

        }
        public Task Ajout(Logement entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public IEnumerable<Logement> GetLogement(int id)
        {
            return GenericRepo.GetAll().Where(e => e.ExperienceId == id).ToList();
        }

        public async Task<Logement> GetLogementById(int id)
        {
            Logement a = await GenericRepo.GetByIdAsync(id);
            return a;
        }

        public Task Update(Logement entity)
        {
            throw new NotImplementedException();
        }
    }
}
