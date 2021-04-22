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
    public class ActiviteRepo : IActiviteRepo
    {
        protected readonly TourMeContext _dbContext;
        readonly private IGenericRepository<Activite> genericRepoUser;
        public ActiviteRepo(TourMeContext dbContext, IGenericRepository<Activite> _GenericRepoActivite)
        {
            _dbContext = dbContext;
            genericRepoUser = _GenericRepoActivite;

        }

        public async Task<Activite> GetActiviteByImage(string src)
        {
            var activite = await _dbContext.Activite.SingleOrDefaultAsync(act=>act.Image==src);
            return activite;
        }

        public async Task Update(Activite entity)
        {
            var Activite = await _dbContext.Activite.SingleAsync(e => e.activiteId == entity.activiteId);
            _dbContext.Entry(Activite).State = EntityState.Detached;
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
