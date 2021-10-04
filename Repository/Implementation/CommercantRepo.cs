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
   public class CommercantRepo : ICommercantRepo
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
        public IEnumerable<EmployeDocuments> GetListfile(string id)
        {


            return dbContext.EmployeDocuments.ToList().Where(x => x.Commerçant.Id == id);
        }
        public async Task<Commerçant> GetCommercantDetailsAsync(string id)
        {
            var commerçant = await dbContext.Commercant.Include(x => x.EmployeDocuments).SingleOrDefaultAsync(c => c.Id == id);
            dbContext.Entry(commerçant).Collection(x => x.EmployeDocuments).Query().Load();
            dbContext.Entry(commerçant).State = EntityState.Detached;
            return commerçant;
        }

    }
}
