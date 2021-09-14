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
   public class HôteRepo :IHôteRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Hôte> genericRepoCommerçant;

        public HôteRepo(TourMeContext dbContext, IGenericRepository<Hôte> _GenericRepoCommerçant)
        {
            this.dbContext = dbContext;
            genericRepoCommerçant = _GenericRepoCommerçant;
        }

        public IQueryable<Hôte> GetAllCommercant()
        {
            var commercant = dbContext.Commercant.Where(ex => ex.Id != null).Include(x => x.EmployeDocuments);
            return commercant;
        }
        public IEnumerable<HôteDocuments> GetListfile(string id)
        {


            return dbContext.EmployeDocuments.ToList().Where(x => x.Hôte.Id == id);
        }

        public async Task<Hôte> GetCommercantDetailsAsync(string id)
        {
            var commerçant = await dbContext.Commercant.Include(x => x.EmployeDocuments).SingleAsync(c => c.Id == id);
            dbContext.Entry(commerçant).Collection(x => x.EmployeDocuments).Query().Load();
            dbContext.Entry(commerçant).State = EntityState.Detached;
            return commerçant;
        }
    }
}
