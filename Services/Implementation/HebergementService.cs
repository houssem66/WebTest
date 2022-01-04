using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
    public class HebergementService : IHebergementService
    {
        private readonly IGenericRepository<Hebergement> genericRepo;
        private readonly IHebergementRepo hebergementRepo;

        public HebergementService(IGenericRepository<Hebergement> genericRepo, IHebergementRepo HebergementRepo)
        {
            this.genericRepo = genericRepo;
            hebergementRepo = HebergementRepo;
        }
        public Task Ajout(Hebergement entity)
        {
            return hebergementRepo.InsertHebergement(entity);
        }

        public Task DeleteHebergementAsnc(int id)
        {
            return genericRepo.DeleteAsync(id);
        }

        public Logement GetHebergement(int id)
        {
            throw new System.NotImplementedException();
        }

        public  Task<Hebergement> GetHebergementById(int id)
        {
            
            return genericRepo.GetByIdAsync(id);
        }

        public Task Update(Hebergement entity)
        {
            return genericRepo.PutAsync(entity.Id, entity);
        }
    }
}
