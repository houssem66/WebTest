using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using Domaine.Entities;

namespace Services.Implementation
{
    public class CommercantService : ICommercantService
    {
        private readonly IGenericRepository<Hôte> genericRepo;
        private readonly IHôteRepo commerçantRepo;

        public CommercantService(IGenericRepository<Hôte> genericRepo, IHôteRepo CommerçantRepo)
        {
            this.genericRepo = genericRepo;
            commerçantRepo = CommerçantRepo;
        }
        public Task Ajout(Hôte commerçant)
        {
            return genericRepo.InsertAsync(commerçant);
        }

        public Task Delete(Hôte Commerçant)
        {
            return genericRepo.DeleteAsync(Commerçant.Id);
        }

        public IList<Hôte> GetAllCommerçants()
        {
            return commerçantRepo.GetAllCommercant().ToList();
        }

        public Task<Hôte> GetCommerçantById(string id)
        {
            return commerçantRepo.GetCommercantDetailsAsync(id);
        }

        public Task Update(Hôte Commerçant)
        {
            return genericRepo.PutAsync(Commerçant.Id, Commerçant);
        }
        public IEnumerable<HôteDocuments> GetListfile(string id)
        {
            return commerçantRepo.GetListfile(id);
        }
    }
}
