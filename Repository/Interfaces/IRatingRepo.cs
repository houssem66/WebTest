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
        public Task<Rating> Rater(Rating rating, int idExperience, string IdUtilisateur,int note);
        public  Task<Decimal> AverageRating(Rating rating,int IdExperience);
    }
}
