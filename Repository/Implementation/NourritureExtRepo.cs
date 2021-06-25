using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data;
using TourMe.Data.Entities;
namespace Repository.Implementation
{
   public class NourritureExtRepo:INourritureExtRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<ServiceNouritture> genericRepoRate;

        public NourritureExtRepo(TourMeContext dbContext, IGenericRepository<ServiceNouritture> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }

        public IQueryable<ServiceNouritture> GetAll()
        {
            var Experience = dbContext.ServiceNourittures.Where(exp => exp.Id != 0).Include(x => x.Documents);

            return Experience;
        }

        public async Task<ServiceNouritture> GetNourritureDetailsAsync(int id)
        {
            var nourriture = await dbContext.ServiceNourittures.Include(x => x.Documents).SingleAsync(e => e.Id == id);
            return nourriture;
        }
    }
}
