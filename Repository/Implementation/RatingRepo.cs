using Domaine.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IEnumerable<Rating> GetListRatingByExp(Experience exp)
        {
            

            return _dbContext.Ratings.ToList().Where(x=>x.experience==exp);
        }
        public async Task<Decimal> AverageRating(int idExperience)
        {


            var x =  _dbContext.Ratings.ToList().Where(x => x.ExperienceId == idExperience);
           if (x.Count() == 0)
            {
                return 0;
            }
            decimal s = 0;
            string ch = "";
            char c ='1';
            int c1 = 0;
           foreach (var item in x)
            {if (item.note != null) { 
                ch = item.note;
                c = ch[0];
                c1 = c - '0';
                s = s + c1;
                }



            }
            
            return s/x.Count();
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
                UtilisateurId = user.Id
               
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

        public async Task Commenter(Rating entity, int idExperience, string IdUtilisateur, string Commentaire)
        {

            var rating = await _dbContext.Ratings.SingleOrDefaultAsync(rat => rat.ExperienceId == entity.ExperienceId && rat.UtilisateurId == entity.UtilisateurId);
            rating.Commentaire = Commentaire;
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
    }
}
