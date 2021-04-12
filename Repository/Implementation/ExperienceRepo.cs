using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        readonly private IGenericRepository<Experience> genericRepoUser;

        public ExperienceRepo(TourMeContext dbContext, IGenericRepository<Experience> _GenericRepoExperience)
        {
           
            _dbContext = dbContext;
            genericRepoUser = _GenericRepoExperience;
        }

      


        public IQueryable<Experience> GetAllExperienceAsync()
        {
            var Experience = _dbContext.Experience.Where(exp => exp.ExperienceId != 0).Include(x=>x.Ratings);

            return Experience;
        }

        public async Task<Experience> GetExperienceDetailsAsync(int id)
        {
            var Experience = await _dbContext.Experience.Include(x=>x.Activites).SingleAsync(Experience => Experience.ExperienceId == id);

            _dbContext.Entry(Experience).Collection(experience=>experience.Activites).Query().Load();
            _dbContext.Entry(Experience).Collection(experience => experience.Ratings).Query().Load();
            _dbContext.Entry(Experience).Collection(experience => experience.Ratings).Query().Include(x => x.utilisateur).Load();
            _dbContext.Entry(Experience).State = EntityState.Detached;

            return Experience;
        }

    

        public async Task PutExperienceAsync(int id, Experience entity)
        {
            var Experience = await _dbContext.Experience.SingleAsync(e => e.ExperienceId == entity.ExperienceId);
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

        public  Experience BestExperience() 
        {


            var Experiences = genericRepoUser.GetAll();
            
            int best = 0;
            int i = 0;
            Experience exp = new Experience();
            //Debug.WriteLine("experience.count :" + Experiences.Count());
            var list = Experiences.ToList();
            foreach(var item in list)
            {
               
                //Debug.WriteLine("value of normal: " + item.Rating);
                //Debug.WriteLine("value of i " + i);
                i++;
                
                //if (!(Experiences.ElementAt(i).Rating==null)) { 
                //if (Experiences.ElementAt(i).Rating [0]> best)
                //{ 
                //    best = Experiences.ElementAt(i).Rating[0];

                //    exp = Experiences.ElementAt(i);
                //        Debug.WriteLine("rating value for best exp " + exp.Rating);
                //    }
                //}

            }

            return exp;
        }

    }
}
