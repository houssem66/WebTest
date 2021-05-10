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
   public class TransportRepo : ITransportRepo
    {
        protected readonly TourMeContext _dbContext;
        readonly private IGenericRepository<Transport> genericRepoNourriture;
        public TransportRepo(TourMeContext dbContext, IGenericRepository<Transport> _GenericRepoNourriture)
        {

            _dbContext = dbContext;
            genericRepoNourriture = _GenericRepoNourriture;
        }

        public  async Task Update(Transport entity)
        {
            var nourriture = await _dbContext.Transports.FindAsync(entity.TrasportId);
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
