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

        public Task<Experience> ExperienceGet(Experience entity)
        {
            return _dbContext.Experience.SingleAsync(Experience => Experience.ExperienceId == entity.ExperienceId);
          
        }

        public IEnumerable<Experience> GetAllExperienceAsync()
        {
            var Experience = _dbContext.Experience.Where(exp => exp.ExperienceId != 0);

            return Experience;
        }

        public async Task<Experience> GetExperienceDetailsAsync(int id)
        {
            Experience Experience = await _dbContext.Experience.SingleAsync(Experience => Experience.ExperienceId == id);

            _dbContext.Entry(Experience).Collection(experience => experience.Activites).Query().Load();
            _dbContext.Entry(Experience).Collection(experience => experience.Ratings).Query().Load();
            _dbContext.Entry(Experience).Collection(experience => experience.Ratings).Query().Include(x => x.utilisateur).Load();
            _dbContext.Entry(Experience).State = EntityState.Detached;

            return Experience;
        }

        public async Task<int> InsertExperience(Experience entity)
        {
         
                _dbContext.Add(entity);
                
              await _dbContext.SaveChangesAsync();
                Experience experience =  _dbContext.Experience.SingleOrDefault(x => x.Titre == entity.Titre && x.Saison == entity.Saison&&x.Lieu==entity.Lieu&&x.TypeExperience==entity.TypeExperience);

                return experience.ExperienceId;

         
        
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


    }
}
