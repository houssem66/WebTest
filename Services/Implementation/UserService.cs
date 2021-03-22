using Domaine.Entities;
using Repository.Implementation;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        readonly private IGenericRepository<Utilisateur> GenericRepo;
        readonly private IUserRepo UserRepo;
        public UserService(IGenericRepository<Utilisateur> _GenericRepo, IUserRepo _CompanyRepo)
        {
            GenericRepo = _GenericRepo;
            UserRepo = _CompanyRepo;
        }
        public IEnumerable<Utilisateur> GetAllUtilisateurs()
        {
            return GenericRepo.GetAll();
        }

        public Task InsertUtilisateurAsync(Utilisateur entity)
        {
            return GenericRepo.InsertAsync(entity);
        }
       

        public Task DeleteUtilisateurAsync(string id)
        {
            return GenericRepo.DeleteAsync(id);
        }




        public IEnumerable<Utilisateur> GetAllUtilisateurDetails()
        {
            return UserRepo.GetAllUserAsync();
        }


        public Task PutUtilisateurAsync(string id, Utilisateur entity)
        {
            return UserRepo.PutUserAsync(id, entity);
        }

        public Task UpdateUtilisateurAsync(string id, Utilisateur entity)
        {
            return UserRepo.PutUserAsync(id, entity);
        }
        public Task<Utilisateur> GetById(string id)
        {
            return UserRepo.GetUserDetailsAsync(id);
        }

        public Task<Utilisateur> GetUtilisateurByIdAsync(string id)
        {
            return GenericRepo.GetByIdAsync(id);
        }












        
    }
}
