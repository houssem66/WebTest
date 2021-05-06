using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data;
using TourMe.Data.Entities;
using Domaine.Entities;

namespace Repository.Implementation
{
   public class CommercantRepo :ICommercantRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Commerçant> genericRepoCommerçant;

        public CommercantRepo(TourMeContext dbContext, IGenericRepository<Commerçant> _GenericRepoCommerçant)
        {
            this.dbContext = dbContext;
            genericRepoCommerçant = _GenericRepoCommerçant;
        }

      
    }
}
