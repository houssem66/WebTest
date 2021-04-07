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
    public class ExperienceRepo : IExperienceRepo
    {

        protected readonly TourMeContext _dbContext;

        public ExperienceRepo(TourMeContext dbContext)
        {
            _dbContext = dbContext;
        }

      

        public IEnumerable<Experience> GetAllExperienceAsync()
        {
            var Experience = _dbContext.Experience.Where(exp => exp.ExperienceId != 0).Include(x=>x.Ratings);

            return Experience;
        }

        public async Task<Experience> GetExperienceDetailsAsync(int id)
        {
            var Experience = await _dbContext.Experience.Include(x=>x.Activites).SingleAsync(Experience => Experience.ExperienceId == id);

            _dbContext.Entry(Experience).Collection(experience=>experience.Activites).Query();



            return Experience;
        }

    

        public async Task PutExperienceAsync(int id, Experience entity)
        {
            var Experience = await _dbContext.Experience.SingleAsync(Experience => Experience.ExperienceId == entity.ExperienceId);
            _dbContext.Entry(Experience).State = EntityState.Detached;
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

        public Experience BestExperience() 
        {


            var Experiences = _dbContext.Experience.Where(exp => exp.ExperienceId != 0).Include(x => x.Ratings);
            int best = 0;
            Experience exp = new Experience();
            foreach(var item in Experiences)
            {
                if (item.Rating[0]>best)
                {
                    best = item.Rating[0];

                    exp = item;
                }


            }

            return exp;
        }

    }
}
