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
    public class RatingRepo : IRatingRepo
    {
        protected readonly TourMeContext _dbContext;
        private readonly IGenericRepository<Rating> genericRepoRate;
        protected DbSet<Rating> DbSet;

        public RatingRepo(TourMeContext dbContext,IGenericRepository<Rating> _GenericRepoRate)
        {
            _dbContext = dbContext;
            genericRepoRate = _GenericRepoRate;
        }
        public async Task Rater(Rating entity, int idE, string IdU, string rate)
        {
            var rating = await _dbContext.Ratings.SingleOrDefaultAsync(rat => rat.ExperienceId == entity.ExperienceId && rat.UtilisateurId == entity.UtilisateurId);
            rating.note = rate;
            _dbContext.Entry(rating).State = EntityState.Detached;
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }


        }
        public async Task<Decimal> AverageRating(int idExperience)
        {




            return (0);
        }

        public async Task<Rating> GetByIDasync(int IDE,string IDU)
        {
            var rating = await _dbContext.Ratings.SingleOrDefaultAsync(rat => rat.ExperienceId == IDE && rat.UtilisateurId == IDU);

            return rating;
        }
        public async Task<Rating> CreateRating(Experience exp, Utilisateur user)
        {
            Rating rating = new Rating
            {
                ExperienceId = exp.ExperienceId,
                UtilisateurId = user.Id,
                utilisateur=user,
                experience=exp
               

            };
            try
            {
              await  genericRepoRate.InsertAsync(rating);
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
            return rating;
        }
    }
}
