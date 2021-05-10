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
    public class NourritureService : INourritureService
    {
        private readonly IGenericRepository<Nourriture> GenericRepo;
        readonly private INourritureRepo NourritureRepo;
        public NourritureService(IGenericRepository<Nourriture> genericRepo, INourritureRepo nourritureRepo )
        {
            NourritureRepo = nourritureRepo;
            GenericRepo = genericRepo;
        }
        public Task Ajout(Nourriture entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public Nourriture GetNourriture(int id)
        {
            var a = GenericRepo.GetAll().Where(e => e.ExperienceId == id).ToList().First();
            return a ;
        }

        public async Task<Nourriture> GetNourritureById(int id)
        {
            Nourriture a = await GenericRepo.GetByIdAsync(id);
            return a;
        }

        public Task Update(Nourriture entity)
        {
            return NourritureRepo.Update(entity);
        }
    }
}
