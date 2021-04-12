using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
    public interface IRatingRepo
    {
        public Task Rater(Rating entity,int idExperience, string IdUtilisateur, string note);
        public Task<Decimal> AverageRating(int IdExperience);

        public Task<Rating> GetByIDasync( int IdExperience,string IdUtilisateur);
        public Task<Rating> CreateRating(Experience exp, Utilisateur user);
        public IEnumerable<Rating> GetListRatingByExp(Experience exp);

        public Task Commenter(Rating entity, int idExperience, string IdUtilisateur, string Commentaire);
    }
}
