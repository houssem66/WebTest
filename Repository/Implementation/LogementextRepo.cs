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
   public class LogementextRepo :ILogementextRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<ServiceLogment> genericRepoRate;

        public LogementextRepo(TourMeContext dbContext, IGenericRepository<ServiceLogment> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }

        public IQueryable<ServiceLogment> GetAll()
        {
            var Experience = dbContext.ServiceLogments.Where(exp => exp.Id != 0).Include(x => x.Documents);

            return Experience;
        }

        public async Task<ServiceLogment> GetlogementDetailsAsync(int id)
        {
            var logment = await dbContext.ServiceLogments.Include(x => x.Documents).SingleAsync(e => e.Id == id);
            return logment;
        }
    }
}
