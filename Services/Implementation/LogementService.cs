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
    public class LogementService : ILogementService
    {
        private readonly IGenericRepository<Logement> GenericRepo;
        readonly private ILogementRepo LogementRepo;
        public LogementService(IGenericRepository<Logement> genericRepo, ILogementRepo logementRepo)
        {
            GenericRepo = genericRepo;
            LogementRepo = logementRepo;

        }
        public Task Ajout(Logement entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public Logement GetLogement(int id)
        {
            var a = GenericRepo.GetAll().Where(e => e.ExperienceId == id).ToList().First();
            return a;
        }

        public async Task<Logement> GetLogementById(int id)
        {
            Logement a = await GenericRepo.GetByIdAsync(id);
            return a;
        }

        public Task Update(Logement entity)
        {
            return LogementRepo.Update(entity);
        }
    }
}
