using Domaine.Entities;
using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
   public class RatingService :IRatingService
    {
        readonly private IGenericRepository<Rating> GenericRepo;
        readonly private IRatingRepo RatingRepo;
        public RatingService(IGenericRepository<Rating> genericRepo, IRatingRepo ratingRepo)
        {
            GenericRepo = genericRepo;
            RatingRepo = ratingRepo;

        }
        public IEnumerable<Rating> GetAllExperiences()
        {

            return GenericRepo.GetAll();
        }
        public IEnumerable<Rating> GetListByeEXp(Experience entity)
        {


            return RatingRepo.GetListRatingByExp(entity);
        }
        public Task<Rating> GetRatingByIdAsync(int id)
        {


            return GenericRepo.GetByIdAsync(id);
        }
        public async Task Rater(Experience exp, Utilisateur user, string rate)
        {
            var rating = await RatingRepo.GetByIDasync(exp.ExperienceId,user.Id);
            if (rating==null)
            {
                rating = await RatingRepo.CreateRating(exp, user);
                await RatingRepo.Rater(rating, exp.ExperienceId, user.Id, rate);
            }
            else
            {
                await RatingRepo.Rater(rating, exp.ExperienceId, user.Id, rate);


            }
           
        }
        public Task<decimal> Moyen(int id)
        {


            return RatingRepo.AverageRating(id);

        }

        public async Task Commenter(Experience exp, Utilisateur user, string commentaire)
        {
            var rating = await RatingRepo.GetByIDasync(exp.ExperienceId, user.Id);
            if (rating == null)
            {
                rating = await RatingRepo.CreateRating(exp, user);
                await RatingRepo.Commenter(rating, exp.ExperienceId, user.Id, commentaire);
            }
            else
            {
                await RatingRepo.Commenter(rating, exp.ExperienceId, user.Id, commentaire);


            }
        }
    }
}
