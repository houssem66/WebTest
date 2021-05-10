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
  public  class ReservationRepo :IReservationRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<ServiceLogment> genericRepoRate;

        public ReservationRepo(TourMeContext dbContext, IGenericRepository<ServiceLogment> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }
    }
}
