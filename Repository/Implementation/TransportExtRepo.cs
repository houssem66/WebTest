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
  public  class TransportExtRepo:ITransportExtRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<ServiceTransport> genericRepoRate;

        public TransportExtRepo(TourMeContext dbContext, IGenericRepository<ServiceTransport> _GenericRepoRate)
        {
            this.dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }
        public IQueryable<ServiceTransport> GetAll()
        {
          
            var Experience = dbContext.ServiceTransports.Where(exp => exp.Id != 0).Include(x => x.Fournisseur).Include(w=>w.Documents);
                return Experience;
           
           
        }

        public async Task<ServiceTransport> GetTransportDetailsAsync(int id)
        {
            var nourriture = await dbContext.ServiceTransports.Include(x => x.Documents).SingleAsync(e => e.Id == id);
            return nourriture;
        }
    }
}
