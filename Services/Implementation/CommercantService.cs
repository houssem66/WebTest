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
        private readonly IGenericRepository<Commerçant> genericRepo;
        private readonly ICommercantRepo commerçantRepo;

        public CommercantService(IGenericRepository<Commerçant> genericRepo, ICommercantRepo CommerçantRepo)
        {
            this.genericRepo = genericRepo;
            commerçantRepo = CommerçantRepo;
        }
        public Task Ajout(Commerçant commerçant)
        {
            return genericRepo.InsertAsync(commerçant);
        }

        public Task Delete(Commerçant Commerçant)
        {
            return genericRepo.DeleteAsync(Commerçant.Id);
        }

        public IList<Commerçant> GetAllCommerçants()
        {
            return genericRepo.GetAll().ToList();
        }

        public Task<Commerçant> GetCommerçantById(string id)
        {
            return genericRepo.GetByIdAsync(id);
        }

        public Task Update(Commerçant Commerçant)
        {
            return genericRepo.PutAsync(Commerçant.Id, Commerçant);
        }
    }
}
