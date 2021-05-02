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
    public class NourritureService : INourritureService
    {
        private readonly IGenericRepository<Nourriture> GenericRepo;
        public NourritureService(IGenericRepository<Nourriture> genericRepo)
        {
            GenericRepo = genericRepo;
        }
        public Task Ajout(Nourriture entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public IEnumerable<Nourriture> GetNourriture(int id)
        {
            return GenericRepo.GetAll().Where(e => e.ExperienceId == id).ToList();
        }

        public async Task<Nourriture> GetNourritureById(int id)
        {
            Nourriture a = await GenericRepo.GetByIdAsync(id);
            return a;
        }

        public Task Update(Nourriture entity)
        {
            throw new NotImplementedException();
        }
    }
}
