using Repository.Implementation;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task DeleteExperienceAsync(string id)
        {
            return GenericRepo.DeleteAsync(id);
        }

        public IEnumerable<Experience> GetAllExperienceDetails()
        {
            return ExperienceRepo.GetAllExperienceAsync();
        }

        public IEnumerable<Experience> GetAllExperiences()
        {
            return GenericRepo.GetAll();
        }

        public Task<Experience> GetById(string id)
        {
            return ExperienceRepo.GetExperienceDetailsAsync(id);
        }

        public Task<Experience> GetExperienceByIdAsync(string id)
        {
            return GenericRepo.GetByIdAsync(id);
        }

        public Task InsertExperienceAsync(Experience entity)
        {
            return GenericRepo.InsertAsync(entity);
        }

        public Task PutExperienceAsync(string id, Experience entity)
        {
            return ExperienceRepo.PutExperienceAsync(id, entity);
        }

        public IEnumerable<Experience> Search(string searchTerm=null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return GenericRepo.GetAll();
            }

            return GenericRepo.GetAll().Where(e => e.Titre.ToLower().Contains(searchTerm.ToLower()) ||
                                            e.TypeExperience.ToLower().Contains(searchTerm.ToLower()) ||e.Saison.ToLower().Contains(searchTerm.ToLower()) ).ToList();
        }

    }
}
