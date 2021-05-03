using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
 public  interface IExperienceService
    {
        public IEnumerable<Experience> GetAllExperiences();

        IEnumerable<Experience> Search(string searchTerm);
        public Task InsertExperienceAsync(Experience entity);
        public Task DeleteExperienceAsync(int id);
        public IEnumerable<Experience> GetAllExperienceDetails(string searchTerm);

      
        public Task PutExperienceAsync(int id, Experience entity);
        

        public Task<Experience> GetExperienceByIdAsync(int id);
        public Task<Experience> GetById(int id);

        public Task<int> InsertExperience(Experience entity);

        public Experience BestExperience();

        public IList<Experience> BestExperiences();
        public IList<Experience> GetExperienceByUser(string id);
    }
}
