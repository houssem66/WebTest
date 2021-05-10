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
    public class NourritureRepo : INourritureRepo
    {
        protected readonly TourMeContext _dbContext;
        readonly private IGenericRepository<Nourriture> genericRepoUser;

        public NourritureRepo(TourMeContext dbContext, IGenericRepository<Nourriture> _GenericRepoNourriture)
        {
            _dbContext = dbContext;
            genericRepoUser = _GenericRepoNourriture;

        }

        public async Task Update(Nourriture entity)
        {
            var nourriture = await _dbContext.Nourritures.FindAsync(entity.NourritureId);
            _dbContext.Entry(nourriture).State = EntityState.Detached;
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
