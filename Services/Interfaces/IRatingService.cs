using Domaine.Entities;
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

        public Task<Rating> GetRatingByIdAsync(int id);
        public Task Rater( Experience exp, Utilisateur user, string rate);
        public Task Commenter(Experience exp, Utilisateur user, string commentaire);
        public Task<decimal> Moyen(int idE);
        public IEnumerable<Rating> GetListByeEXp(Experience entity);

    }
}
