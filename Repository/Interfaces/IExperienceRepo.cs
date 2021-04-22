using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
   public interface IExperienceRepo
    {
        public Task<Experience> GetExperienceDetailsAsync(int id);
        public Task PutExperienceAsync(int id, Experience entity);

      
        public Task<int> InsertExperience(Experience entity);
        public Task<Experience> ExperienceGet(Experience entity);

        public IQueryable<Experience> GetAllExperienceAsync();
        public Experience BestExperience();
        public IList<Experience> GetThreeBest();


    }
}
