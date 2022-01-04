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
    public class HebergementRepo:IHebergementRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Hebergement> genericRepoRate;

        public HebergementRepo(TourMeContext dbContext, IGenericRepository<Hebergement> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }

        public async Task<Hebergement> GetHebergementDetailsAsync(int Id)
        {
            var hebergement = await dbContext.Hebergements.Include(x => x.Commercant).SingleOrDefaultAsync(c => c.Id == Id);
            return hebergement;
        }

        public async Task InsertHebergement(Hebergement entity)
        {
            dbContext.Add(entity);
            dbContext.Entry(entity.Commercant).State = EntityState.Detached;
            dbContext.Entry(entity.Commercant).State = EntityState.Unchanged;
            await dbContext.SaveChangesAsync();
        }
    }
}
