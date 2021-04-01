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

        public Task<Rating> GetRatingByIdAsync(int id)
        {


            return GenericRepo.GetByIdAsync(id);
        }
        public async Task Rater(int idE, string IdU, string rate)
        {
            var rating = await RatingRepo.GetByIDasync(idE,IdU);
            if (rating==null)
            {
                rating = await RatingRepo.CreateRating(idE, IdU);
            
            }
            else
            {
                await RatingRepo.Rater(rating,idE,IdU,rate);


            }
           
        }
        public Task Moyen(int id)
        {


            return RatingRepo.AverageRating(id);

        }
        
    }
}
