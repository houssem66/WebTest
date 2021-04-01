using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
    public interface IRatingService
    {
        public IEnumerable<Rating> GetAllExperiences();

        public Task<Rating> GetExperienceByIdAsync(int id);
        public Task Rater(int idE, string IdU, int rate);
        public Task Moyen(int idE);
       
    }
}
