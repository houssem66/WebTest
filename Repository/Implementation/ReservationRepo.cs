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
    public class ReservationRepo : IReservationRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Panier> genericRepoRate;
      //  private readonly DbSet<Panier> dbSet;

        public ReservationRepo(TourMeContext _dbContext, IGenericRepository<Panier> _GenericRepoRate/*, DbSet<Panier> DbSet*/)
        {
            dbContext = _dbContext;
            genericRepoRate = _GenericRepoRate;
          //  dbSet = DbSet;
        }

        public async Task InsertAsync(Panier panier, int id)

        {
            try
            {
                var experiences =await  dbContext.Experience.SingleAsync(p => p.ExperienceId.Equals(id));
                panier.Experiences.Add(experiences);
                await dbContext.Paniers.AddAsync(panier);
            


                await dbContext.SaveChangesAsync();
            }
            catch
            {




            }


        }
           

}

        
}

