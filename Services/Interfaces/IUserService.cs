using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public interface IUserService
    {
        public IEnumerable<Utilisateur> GetAllUtilisateurs();


        public Task InsertUtilisateurAsync(Utilisateur Utilisateur);
        public Task DeleteUtilisateurAsync(string id);
        public IEnumerable<Utilisateur> GetAllUtilisateurDetails();


        public Task PutUtilisateurAsync(string id, Utilisateur Utilisateur);
        public Task UpdateUtilisateurAsync(string id, Utilisateur Utilisateur);

        public Task<Utilisateur> GetUtilisateurByIdAsync(string id);
        public Task<Utilisateur> GetById(string id);





    }
}
