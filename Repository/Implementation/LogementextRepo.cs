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
    }
}
