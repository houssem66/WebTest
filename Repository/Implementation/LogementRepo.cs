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
   public class LogementRepo :ILogementRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Logement> genericRepoRate;

        public LogementRepo(TourMeContext dbContext, IGenericRepository<Logement> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }

        public async Task Update(Logement entity)
        {
            var Logement = await dbContext.Logements.FindAsync(entity.LogementId);
            dbContext.Entry(Logement).State = EntityState.Detached;
            dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
