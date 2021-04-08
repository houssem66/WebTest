using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
    public class ExperienceService : IExperienceService
    {
        readonly private IGenericRepository<Experience> GenericRepo;
        readonly private IExperienceRepo ExperienceRepo;

        public ExperienceService(IGenericRepository<Experience> genericRepo, IExperienceRepo experienceRepo)
        {
            GenericRepo = genericRepo;
            ExperienceRepo = experienceRepo;
        }

   
        public Task DeleteExperienceAsync(int id)
        {
            return GenericRepo.DeleteAsync(id);
        }

        public IQueryable<Experience> GetAllExperienceDetails(string searchTerm = null)
        {
            if (!(string.IsNullOrEmpty(searchTerm)))
            {if (searchTerm.Length == 1)
                {
                    return null;

                }
                return ExperienceRepo.GetAllExperienceAsync().Where(e => e.Titre.ToLower().Contains(searchTerm.ToLower()) ||
                                            e.TypeExperience.ToLower().Contains(searchTerm.ToLower()) || e.Saison.ToLower().Contains(searchTerm.ToLower()));
            }

            return ExperienceRepo.GetAllExperienceAsync();
        }

        public IEnumerable<Experience> GetAllExperiences()
        {
            return GenericRepo.GetAll();
        }

        public Task<Experience> GetById(int id)
        {
            return ExperienceRepo.GetExperienceDetailsAsync(id);
        }

        public Task<Experience> GetExperienceByIdAsync(int id)
        {
            return GenericRepo.GetByIdAsync(id);
        }

        public Task InsertExperienceAsync(Experience entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public Task PutExperienceAsync(int id, Experience entity)
        {
            return ExperienceRepo.PutExperienceAsync(id,entity);
        }

        public IEnumerable<Experience> Search(string searchTerm=null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return GenericRepo.GetAll();
            }
            if (searchTerm.Length==1)
            {
                Debug.WriteLine("the value of searchSterm is : "+searchTerm );
                return GenericRepo.GetAll(); 
            }

            return GenericRepo.GetAll().Where(e => e.Titre.ToLower().Contains(searchTerm.ToLower()) ||
                                            e.TypeExperience.ToLower().Contains(searchTerm.ToLower()) ||e.Saison.ToLower().Contains(searchTerm.ToLower()) ).ToList();
        }

        public Experience BestExperience()
        {

            return ExperienceRepo.BestExperience();

        }
    }
}
