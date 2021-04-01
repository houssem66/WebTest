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
  public  class RatingRepo : IRatingRepo
    {
        protected readonly TourMeContext _dbContext;
        protected DbSet<Rating> DbSet;
        public RatingRepo(TourMeContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<Rating> Rater( int idE, string IdU,int rate)
        {
            var rating = await _dbContext.Ratings.SingleAsync(rat=>rat.ExperienceId==idE&&rat.UtilisateurId==IdU);
            if (rating==null) { 
            Rating rating2 = new Rating
            {
                ExperienceId = idE,
                UtilisateurId = IdU,
                note = ""
            };
                try
                {
                    DbSet.Add(rating);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw new NotImplementedException();
                }
            }
            else
            {
                _dbContext.Entry(rating).State = EntityState.Detached;

            }

           


            return rating;
        }
        public async Task<Decimal> AverageRating( int idExperience)
        {
            

                

            return (0);
        }
    }
}
