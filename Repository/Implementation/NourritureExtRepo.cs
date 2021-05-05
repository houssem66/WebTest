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
    }
}
