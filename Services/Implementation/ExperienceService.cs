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

        public IEnumerable<Experience> GetAllExperienceDetails(string searchTerm = null)
        {
            if (!(string.IsNullOrEmpty(searchTerm)))
            {if (searchTerm.Length == 1)
                { var ch = searchTerm[0];


                    Debug.WriteLine("the value of searchSterm is : " + ch);
                    return ExperienceRepo.GetAllExperienceAsync().Where(e => e.AvgRating.StartsWith (searchTerm));
                    //return from Experience in ExperienceRepo.GetAllExperienceAsync()
                    //       where Experience.AvgRating.First().Equals(ch)
                    //       select Experience;
                }
                return ExperienceRepo.GetAllExperienceAsync().Where(e => e.Titre.ToLower().Contains(searchTerm.ToLower()) ||
                                            e.TypeExperience.ToString().ToLower().Contains(searchTerm.ToLower()) || e.Saison.ToLower().Contains(searchTerm.ToLower())||e.Lieu.ToLower().Contains(searchTerm.ToLower()));
            }

            return ExperienceRepo.GetAllExperienceAsync();
        }
        public IEnumerable<Experience> GetAllExperienceDetails()
        {
            

            return ExperienceRepo.GetAllExperienceAsync();
        }

        public IEnumerable<Experience> GetAllExperiences()
        {
            return GenericRepo.GetAll();
        }

        public async Task<Experience> GetById(int id)
        {
            Experience Experience = await ExperienceRepo.GetExperienceDetailsAsync(id);

           


            return Experience;
        }

        public Task<Experience> GetExperienceByIdAsync(int id)
        {
            return ExperienceRepo.GetExperienceDetailsAsync(id);
        }

        public async Task<int> InsertExperience(Experience entity)
        {
            return await ExperienceRepo.InsertExperience(entity);
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
            var l = searchTerm.Length;
            if (l>1)
            {
                Debug.WriteLine("the value of searchSterm is : "+searchTerm );
                return GenericRepo.GetAll().Where(e => e.Titre.ToLower().Contains(searchTerm.ToLower()) ||
                                           e.TypeExperience.ToString().ToLower().Contains(searchTerm.ToLower()) || e.Saison.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            else
            {
                
                return GenericRepo.GetAll().Where(e => e.Titre.ToLower().Contains(searchTerm.ToLower()) ||
                                           e.TypeExperience.ToString().ToLower().Contains(searchTerm.ToLower()) || e.Saison.ToLower().Contains(searchTerm.ToLower())).ToList();

            }

           
        }

        public Experience BestExperience()
        {

            return ExperienceRepo.BestExperience();

        }

        public IList<Experience> BestExperiences()
        {
            return ExperienceRepo.GetThreeBest();
        }

        public  IList<Experience> GetExperienceByUser(string id)
        {
            return ExperienceRepo.GetAllExperienceAsync().Where(e => e.CommerçantId.Equals(id)).ToList();
          
        }
    }
}
