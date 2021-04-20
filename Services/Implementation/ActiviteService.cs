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
  public   class ActiviteService : IActiviteService
    {
        private readonly IGenericRepository<Activite> GenericRepo;
        readonly private IActiviteRepo ActiviteRepo;
        public ActiviteService(IGenericRepository<Activite> genericRepo, IActiviteRepo activiteRepo)
        {
            GenericRepo = genericRepo;
          ActiviteRepo = activiteRepo ;
        }
        public  Task Ajout(Activite activite)
        {
            return GenericRepo.InsertAsync(activite);
        }

        public IEnumerable<Activite> GetActivite(int id)
        { 
            return GenericRepo.GetAll().Where(e => e.ExperienceId==id).ToList();
        }

        public async Task<Activite> GetActiviteById(int id)
        {
           Activite a = await GenericRepo.GetByIdAsync(id);
            return a;
        }

        public Task<Activite> GetActiviteByImage(string src)
        {
            return ActiviteRepo.GetActiviteByImage(src);
        }

        public Task Update(Activite activite)
        {
            return  ActiviteRepo.Update(activite);
        }
    }
}

