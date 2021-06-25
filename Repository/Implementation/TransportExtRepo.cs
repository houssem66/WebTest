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
  public  class TransportExtRepo:ITransportExtRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<ServiceTransport> genericRepoRate;

        public TransportExtRepo(TourMeContext dbContext, IGenericRepository<ServiceTransport> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }
    }
}
