using Domaine.Entities;
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
   public class CommercantRepo :ICommercantRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Commerçant> genericRepoCommerçant;

        public CommercantRepo(TourMeContext dbContext, IGenericRepository<Commerçant> _GenericRepoCommerçant)
        {
            this.dbContext = dbContext;
            genericRepoCommerçant = _GenericRepoCommerçant;
        }

        public IQueryable<Commerçant> GetAllCommercant()
        {
            var commercant = dbContext.Commercant.Where(ex => ex.Id != null).Include(x => x.EmployeDocuments);
            return commercant;
        }
    }
}
